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
        public MoveIssueSlipItemToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip.Item> Handle(MoveIssueSlipItemToBinCommand request, CancellationToken cancellationToken)
        {
            IssueSlip.Item item = await this.DatabaseContext.IssueSlipItems.FindAsync(new object[] { request.IssueSlipId, request.WareId }, cancellationToken);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlipItem_EntityNotFoundException"], request.IssueSlipId, request.WareId));
            }

            item.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
