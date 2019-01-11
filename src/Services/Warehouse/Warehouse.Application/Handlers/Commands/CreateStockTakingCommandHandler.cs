using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateStockTakingCommandHandler : IRequestHandler<CreateStockTakingCommand, StockTaking>
    {
        /*
         * It should be able to create StockTaking from the Id of a section.
         * Should be create for each section separately
         */

        public CreateStockTakingCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<StockTaking> Handle(CreateStockTakingCommand request, CancellationToken cancellationToken)
        {
            List<StockTaking.Item> items = new List<StockTaking.Item>(request.Model.Items.Count);

            foreach (CreateStockTakingCommand.CreateStockTakingCommandModel.Item item in request.Model.Items)
            {
                items.Add(new StockTaking.Item(0, item.WareId, item.PositionId, item.CurrentStock, item.CountedStock));
            }

            StockTaking stockTaking = new StockTaking(request.Model.Name, items);

            this.DatabaseContext.StockTakings.Add(stockTaking);
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);
            return stockTaking;
        }
    }
}
