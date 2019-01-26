using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipItemByIssueSlipIdAndWareIdCommandHandler : IRequestHandler<FindIssueSlipItemByIssueSlipIdAndWareIdCommand, IssueSlip.Item>
    {
        protected const string FindIssueSlipItemByIssueSlipIdAndWareIdCommandHandler_EntityNotFoundException = "IssueSlip.Item (IssueSlipId={0}, WareId={1}) not found!";

        public FindIssueSlipItemByIssueSlipIdAndWareIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip.Item> Handle(FindIssueSlipItemByIssueSlipIdAndWareIdCommand request, CancellationToken cancellationToken)
        {
            // Find an IssueSlip.Item and throw an exception if not found
            IssueSlip.Item item = await this.DatabaseContext.IssueSlipItems.FindAsync(new object[]
            {
                request.Model.IssueSlipId,
                request.Model.WareId
            }, cancellationToken);
            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(FindIssueSlipItemByIssueSlipIdAndWareIdCommandHandler_EntityNotFoundException, request.Model.IssueSlipId, request.Model.WareId));
            }

            return item;
        }
    }
}
