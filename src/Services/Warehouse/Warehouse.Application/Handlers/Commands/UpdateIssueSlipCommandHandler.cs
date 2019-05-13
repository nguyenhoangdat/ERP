using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateIssueSlipCommandHandler : IRequestHandler<UpdateIssueSlipCommand, IssueSlip>
    {
        public UpdateIssueSlipCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip> Handle(UpdateIssueSlipCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.IssueSlips.Any(x => x.Id == request.Id))
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_Update_EntityNotFoundException"], request.Id));
            }

            List<IssueSlip.Item> items = new List<IssueSlip.Item>();
            foreach (UpdateIssueSlipCommand.Item item in request.Items)
            {
                items.Add(new IssueSlip.Item(item.IssueSlipId, item.WareId, item.PositionId, item.RequstedUnits, item.IssuedUnits));
            }

            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.Update(new IssueSlip(request.OrderId, request.UtcDispatchDate, request.UtcDeliveryDate, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new IssueSlipUpdatedDomainEvent(issueSlip), cancellationToken);

            return issueSlip;
        }
    }
}
