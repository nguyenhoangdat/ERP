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
            if (!this.DatabaseContext.IssueSlips.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_Update_EntityNotFoundException"], request.Model.Id));
            }

            List<IssueSlip.Item> items = new List<IssueSlip.Item>();
            foreach (UpdateIssueSlipCommand.UpdateIssueSlipCommandModel.Item item in request.Model.Items)
            {
                items.Add(new IssueSlip.Item(item.IssueSlipId, item.WareId, item.PositionId, item.RequstedUnits, item.IssuedUnits, item.EmployeeId));
            }

            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.Update(new IssueSlip(request.Model.OrderId, request.Model.UtcDispatchDate, request.Model.UtcDeliveryDate, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new IssueSlipUpdatedDomainEvent(issueSlip), cancellationToken);

            return issueSlip;
        }
    }
}
