using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreIssueSlipItemFromBinCommandHandler : IRequestHandler<RestoreIssueSlipItemFromBinCommand, IssueSlip.Item>
    {
        public RestoreIssueSlipItemFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip.Item> Handle(RestoreIssueSlipItemFromBinCommand request, CancellationToken cancellationToken)
        {
            IssueSlip.Item item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlipId == request.IssueSlipId &&
                x.PositionId == request.PositionId &&
                x.WareId == request.WareId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlipItem_EntityNotFoundException, request.IssueSlipId, request.WareId));
            }
            if (item.IssueSlip.CanBeRestoredFromBin())
            {
                await this.Mediator.Send(new RestoreIssueSlipFromBinCommand(item.IssueSlipId), cancellationToken);
            }
            if (item.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.IssueSlipItem_EntityRestoreFromBinException, request.IssueSlipId, request.PositionId, request.WareId));
            }

            item.UtcMovedToBin = null;
            item.MovedToBinInCascade = false;

            if (item.IssuedUnits < item.RequestedUnits)
            {
                await this.Mediator.Publish(new CreateIssueSlipReservationCommand(item.PositionId, item.RequestedUnits - item.IssuedUnits), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new IssueSlipItemRestoredFromBinDomainEvent(item), cancellationToken);

            return item;
        }
    }
}
