using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class OrderStatusChangedToAwaitingValidationIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected IEventBus EventBus { get; }
        protected ILogger<OrderStatusChangedToAwaitingValidationIntegrationEventHandler> Logger { get; }     
        protected IMediator Mediator { get; }

        public OrderStatusChangedToAwaitingValidationIntegrationEventHandler(
            IEventBus eventBus,
            DatabaseContext context,
            ILogger<OrderStatusChangedToAwaitingValidationIntegrationEventHandler> logger,
            IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Logger = logger;
            this.EventBus = eventBus;
            this.Mediator = mediator;
        }

        public async Task Handle(OrderStatusChangedToAwaitingValidationIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        protected bool IsOrderValid(List<OrderStatusChangedToAwaitingValidationIntegrationEvent.OrderItem> items)
        {
            bool valid = true;

            foreach (OrderStatusChangedToAwaitingValidationIntegrationEvent.OrderItem item in items)
            {
                Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                if (ware == null || this.DatabaseContext.Wares.Count(ware) < item.Units)
                {
                    valid = false;
                }                
            }

            return valid;
        }
    }
}
