using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipItemByIdCommandHandler : IRequestHandler<FindIssueSlipItemByIdCommand, IssueSlip.Item>
    {
        public FindIssueSlipItemByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip.Item> Handle(FindIssueSlipItemByIdCommand request, CancellationToken cancellationToken)
        {
            IssueSlip.Item item = this.DatabaseContext.IssueSlipItems.FirstOrDefault(x =>
                x.IssueSlipId == request.IssueSlipId &&
                x.PositionId == request.PositionId &&
                x.WareId == request.WareId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlipItem_EntityNotFoundException, request.IssueSlipId, request.PositionId, request.WareId));
            }

            return item;
        }
    }
}
