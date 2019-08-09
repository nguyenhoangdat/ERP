using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class AssignIssueSlipItemToPositionCommandHandler : IRequestHandler<AssignIssueSlipItemToPositionCommand, IssueSlip.Item>
    {
        public AssignIssueSlipItemToPositionCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip.Item> Handle(AssignIssueSlipItemToPositionCommand request, CancellationToken cancellationToken)
        {
            IQueryable<IssueSlip.Item> items = this.DatabaseContext.IssueSlipItems
                .Where(x =>
                    x.IssueSlipId == request.IssueSlipId &&
                    x.WareId == request.WareId);

            if (items.Any(x => x.PositionId == request.PositionId))
            {
                throw new IssueSlipItemPositionAlreadyAssignedException(string.Format(Resources.Exceptions.Values["IssueSlipItem_PositionAlreadyAssignedException"], request.IssueSlipId, request.PositionId, request.WareId));
            }

            IssueSlip.Item item = items.FirstOrDefault(x => x.PositionId == null);

            if (item == null)
            {
                throw new IssueSlipItemFullyAssignedException(string.Format(Resources.Exceptions.Values["IssueSlipItem_FullyAssignedException"], request.IssueSlipId, request.WareId));
            }

            Position position = this.DatabaseContext.Positions.FirstOrDefault(x => x.Id == request.PositionId);

            if (item.Ware != position.GetWare())
            {
                throw new PositionWareConflictException(string.Format(Resources.Exceptions.Values["IssueSlipItem_PositionWareConflictException"], request.IssueSlipId, request.PositionId, request.WareId));
            }

            item.PositionId = request.PositionId;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
