using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateStockTakingCommandHandler : IRequestHandler<UpdateStockTakingCommand, StockTaking>
    {
        public UpdateStockTakingCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(UpdateStockTakingCommand request, CancellationToken cancellationToken)
        {
            List<StockTaking.Item> items = new List<StockTaking.Item>(request.Model.Items.Count);
            foreach (UpdateStockTakingCommand.UpdateStockTakingCommandModel.Item item in request.Model.Items)
            {
                items.Add(new StockTaking.Item(item.StockTakingId, item.WareId, item.PositionId, item.CurrentStock, item.CountedStock, item.EmployeeId, item.UtcCounted));
            }

            StockTaking stockTaking = this.DatabaseContext.StockTakings.Update(new StockTaking(request.Model.Name, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new StockTakingUpdatedDomainEvent(stockTaking));

            return stockTaking;
        }
    }
}
