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
        protected const string CreateStockTakingForSectionCommandHandler_EntityNotFoundException = "Warehouse(Id={0}) not found!";
        protected const string CreateStockTakingForSectionCommandHandler_StockTakingName = "Stock-Taking in Warehouse(Id={0})";

        public CreateStockTakingForWarehouseCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(CreateStockTakingForWarehouseCommand request, CancellationToken cancellationToken)
        {
            // Ensure that Warehouse with specified Id exists
            Warehouse.Domain.Entities.Warehouse warehouse = this.DatabaseContext.Warehouses.Find(request.Model.WarehouseId);
            if (warehouse == null)
            {
                throw new EntityNotFoundException(string.Format(CreateStockTakingForSectionCommandHandler_EntityNotFoundException, request.Model.WarehouseId));
            }

            // Create Model.Items for Positions in Warehouse
            IEnumerable<Position> positions = this.DatabaseContext.Positions.Where(x => x.Section.WarehouseId == request.Model.WarehouseId).ToList();
            List<CreateStockTakingCommand.CreateStockTakingCommandModel.Item> items = new List<CreateStockTakingCommand.CreateStockTakingCommandModel.Item>();
            foreach (Position item in positions)
            {
                items.Add(new CreateStockTakingCommand.CreateStockTakingCommandModel.Item(item.GetWare().Id, item.Id, item.CountWare(), 0));
            }

            // Create StockTaking through command
            StockTaking stockTaking = await this.Mediator.Send(new CreateStockTakingCommand(string.Format(CreateStockTakingForSectionCommandHandler_StockTakingName, warehouse.Id), items));

            // Publish DomainEvent that the StockTaking has been created
            await this.Mediator.Publish(new StockTakingCreatedDomainEvent(stockTaking));

            return stockTaking;
        }
    }
}
