using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateIssueSlipCommandHandler : IRequestHandler<CreateIssueSlipCommand, IssueSlip>
    {
        public CreateIssueSlipCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip> Handle(CreateIssueSlipCommand request, CancellationToken cancellationToken)
        {
            List<IssueSlip.Item> items = new List<IssueSlip.Item>(request.Model.Items.Count);
            foreach (CreateIssueSlipCommand.CreateIssueSlipCommandModel.Item item in request.Model.Items)
            {
                items.Add(new IssueSlip.Item(0, item.WareId, item.PositionId, item.RequstedUnits, item.IssuedUnits, 0));
            }

            IssueSlip issueSlip = this.DatabaseContext.IssueSlips.Add(new IssueSlip(request.Model.Name, request.Model.UtcDispatchDate, request.Model.UtcDeliveryDate, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return issueSlip;
        }
    }
}
