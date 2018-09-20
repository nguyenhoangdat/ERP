using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.BuildingBlocks.EventBusServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.API.Integration.Events;
using Warehouse.API.Models;
using Warehouse.API.Models.Extensions;

namespace Warehouse.API.Integration.Handlers
{
    public class OrderStatusChangedToAwaitingValidationIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>
    {
        protected DatabaseContext _databaseContext { get; }
        protected IEventBus _eventBus { get; }
        protected ILogger<OrderStatusChangedToAwaitingValidationIntegrationEventHandler> _logger { get; }        

        public OrderStatusChangedToAwaitingValidationIntegrationEventHandler(IEventBus eventBus, DatabaseContext context, ILogger<OrderStatusChangedToAwaitingValidationIntegrationEventHandler> logger)
        {
            this._databaseContext = context;
            this._logger = logger;
            this._eventBus = eventBus;
        }

        public async Task Handle(OrderStatusChangedToAwaitingValidationIntegrationEvent @event)
        {
            if (this.IsOrderValid(@event.OrderItems))
            {
                OrderStockConfirmedIntegrationEvent confirmedIntegrationEvent = new OrderStockConfirmedIntegrationEvent(@event.OrderId);
                this._eventBus.Publish(confirmedIntegrationEvent);

                //TODO: Create IssueSlip and publish an event (IssueSlipCreatedIntegrationEvent)
            }
            else
            {
                OrderStockRejectedIntegrationEvent rejectedIntegrationEvent = new OrderStockRejectedIntegrationEvent(@event.OrderId);
                this._eventBus.Publish(rejectedIntegrationEvent);
            }

            throw new NotImplementedException();
        }

        protected bool IsOrderValid(List<OrderStatusChangedToAwaitingValidationIntegrationEvent.OrderItem> items)
        {
            bool valid = true;

            foreach (var item in items)
            {
                Ware ware = this._databaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                if (ware == null || ware.Count() < item.Units)
                {
                    valid = false;
                }                
            }

            return valid;
        }
    }
}
