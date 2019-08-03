using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
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
            IssueSlip.Item item = await this.DatabaseContext.IssueSlipItems.FindAsync(new object[] { request.IssueSlipId, request.WareId }, cancellationToken);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlipItem_EntityNotFoundException"], request.IssueSlipId, request.WareId));
            }

            item.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            if (item.IssuedUnits < item.RequestedUnits)
            {
                await this.Mediator.Send(new RemoveIssueSlipReservationCommand(item.Position, item.RequestedUnits - item.IssuedUnits), cancellationToken);
            }

            return item;
        }
    }
}
