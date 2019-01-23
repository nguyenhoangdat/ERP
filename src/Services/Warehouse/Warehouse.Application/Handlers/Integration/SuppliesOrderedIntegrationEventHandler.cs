using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class SuppliesOrderedIntegrationEventHandler : IIntegrationEventHandler<SuppliesOrderedIntegrationEvent>
    {
        protected const string SuppliesOrderedIntegrationEventHandlerReceiptName = "Supplies from {0} expected on {1}";
        protected const string SuppliesOrderedIntegrationEventHandlerWareNullReference = "Unable to find Ware with Product Id={0}";
        protected const string SuppliesOrderedIntegrationEventHandlerWarehouseIsFull = "Unable to allocate position for Ware with Id={0} and ammount={1}";

        public SuppliesOrderedIntegrationEventHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<SuppliesOrderedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(SuppliesOrderedIntegrationEvent @event)
        {
            //TODO: Move to Command
            throw new System.NotImplementedException();

            // Create a list of Receipt Items and assign them to positons
            List<Receipt.Item> items = new List<Receipt.Item>(@event.Items.Count);
            foreach (SuppliesOrderedIntegrationEvent.SupplyItem item in @event.Items)
            {
                Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                if (ware == null)
                {
                    this.Logger.Log(LogLevel.Critical, SuppliesOrderedIntegrationEventHandlerWareNullReference, item.ProductId);
                }

                Position position = this.Mediator.Send(new FindPositionForAllocationCommand(@event.WarehouseId, ware.Id, item.Count)).Result;
                if (position == null)
                {
                    this.Logger.Log(LogLevel.Critical, SuppliesOrderedIntegrationEventHandlerWarehouseIsFull, ware.Id, item.Count);
                }

                items.Add(new Receipt.Item(0, position.Id, ware.Id, item.Count, 0, 0));
            }

            // Create parameters for Receipt
            string dateTime = @event.UtcExpected.ToString("{0:ddd dd.MM.yyyy HH:mm}");
            string name = string.Format(SuppliesOrderedIntegrationEventHandlerReceiptName, @event.SupplierName, dateTime);

            // Create a Receipt and save it to database
            Receipt receipt = this.DatabaseContext.Receipts.Add(new Receipt(0, name, @event.UtcExpected, items)).Entity;
            await this.DatabaseContext.SaveChangesAsync();

            // Publish Domain Event (INotification) that the Receipt has been successfully created
            await this.Mediator.Publish(new ReceiptCreatedDomainEvent(receipt));
        }
    }
}
