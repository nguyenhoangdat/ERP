using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveIssueSlipItemToBinCommandHandler : IRequestHandler<MoveIssueSlipItemToBinCommand, IssueSlip.Item>
    {
        public MoveIssueSlipItemToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip.Item> Handle(MoveIssueSlipItemToBinCommand request, CancellationToken cancellationToken)
        {
            IssueSlip.Item item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlipId == request.IssueSlipId &&
                x.PositionId == request.PositionId &&
                x.WareId == request.WareId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlipItem_EntityNotFoundException, request.IssueSlipId, request.PositionId, request.WareId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (item.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.IssueSlipItem_EntityMoveToBinException, request.IssueSlipId, request.PositionId, request.WareId));
                }
            }

            item.UtcMovedToBin = DateTime.UtcNow;
            item.MovedToBinInCascade = request.MovedToBinInCascade;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            if (item.IssuedUnits < item.RequestedUnits)
            {
                await this.Mediator.Send(new RemoveIssueSlipReservationCommand(item.PositionId.Value, item.RequestedUnits - item.IssuedUnits), cancellationToken);
            }

            if (item.IssueSlip.CanBeMovedToBin())
            {
                await this.Mediator.Send(new MoveIssueSlipToBinCommand(item.IssueSlipId, true), cancellationToken);
            }
            
            await this.Mediator.Publish(new IssueSlipItemMovedToBinDomainEvent(item), cancellationToken);

            return item;
        }
    }
}
