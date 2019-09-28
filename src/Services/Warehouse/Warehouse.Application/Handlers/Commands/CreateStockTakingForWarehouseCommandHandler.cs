using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateStockTakingForWarehouseCommandHandler : IRequestHandler<CreateStockTakingForWarehouseCommand, StockTaking>
    {
        public CreateStockTakingForWarehouseCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(CreateStockTakingForWarehouseCommand request, CancellationToken cancellationToken)
        {
            // Ensure that Warehouse with specified Id exists
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.FirstOrDefault(x => x.Id == request.WarehouseId);
            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Warehouse_EntityNotFoundException, request.WarehouseId));
            }

            // Create Model.Items for Positions in Warehouse
            IEnumerable<Position> positions = request.IncludeEmptyPositions
                ? this.DatabaseContext.Positions.Where(x => x.Section.WarehouseId == request.WarehouseId).ToList()
                : this.DatabaseContext.Positions.Where(x => x.Section.WarehouseId == request.WarehouseId && x.CountWare() > 0).ToList();

            List<CreateStockTakingCommand.Item> items = new List<CreateStockTakingCommand.Item>();
            foreach (Position item in positions)
            {
                items.Add(new CreateStockTakingCommand.Item(item.GetWare().Id, item.Id, item.CountWare()));
            }

            // Create StockTaking through command
            StockTaking stockTaking = await this.Mediator.Send(new CreateStockTakingCommand(string.Format(Properties.Resources.StockTaking_Name_Warehouse, warehouse.Id), items), cancellationToken);

            // Publish DomainEvent that the StockTaking has been created
            await this.Mediator.Publish(new StockTakingCreatedDomainEvent(stockTaking), cancellationToken);

            return stockTaking;
        }
    }
}
