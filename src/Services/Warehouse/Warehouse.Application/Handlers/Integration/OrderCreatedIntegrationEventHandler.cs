using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private const string OrderCreatedIntegrationEventHandler_InvalidOrderException = "Unable to create Issue Slip from Order (id={0}). Products were not found!";

        protected DatabaseContext DatabaseContext { get; }
        protected IEventBus EventBus { get; }
        protected ILogger<OrderCreatedIntegrationEventHandler> Logger { get; }     
        protected IMediator Mediator { get; }

        public OrderCreatedIntegrationEventHandler(
            DatabaseContext context,
            IEventBus eventBus,
            ILogger<OrderCreatedIntegrationEventHandler> logger,
            IMediator mediator)
        {
            this.DatabaseContext = context;
            this.EventBus = eventBus;
            this.Logger = logger;            
            this.Mediator = mediator;
        }

        /// <summary>
        /// Handle OrderCreatedIntegrationEvent and create new IssueSlip
        /// </summary>
        /// <param name="event">The instance of OrderCreatedIntegrationEvent</param>
        /// <returns></returns>
        public async Task Handle(OrderCreatedIntegrationEvent @event) //TODO: 2019.2 - Do better (include Warehouses, ...) 
        {
            // Validate Order
            if (!this.IsOrderValid(@event.OrderItems))
            {
                throw new InvalidOrderException(string.Format(OrderCreatedIntegrationEventHandler_InvalidOrderException, @event.OrderId));
            }

            List<IssueSlip.Item> issueSlipItems = new List<IssueSlip.Item>(@event.OrderItems.Count);

            // Foreach OrderItems
            foreach (OrderCreatedIntegrationEvent.OrderItem item in @event.OrderItems)
            {
                // Get Ware by ProductId
                Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                // Get all positions where Ware is stored
                IEnumerable<Position> positions = this.DatabaseContext.Positions
                    .Where(x => x.GetWare() == ware && x.CountWare() > 0)
                    .OrderBy(x => x.Rating)
                    .ThenBy(x => x.Movements.OrderByDescending(m => m.UtcCreated).FirstOrDefault().CountTotal) // Order by TotalCount of Units at Position
                    .ToList();

                int remainingUnits = item.Units;

                foreach (Position position in positions)
                {
                    int units = position.CountWare();

                    if (units < remainingUnits)
                    {
                        remainingUnits -= units;
                        //TODO: Add to RESERVED units
                        issueSlipItems.Add(new IssueSlip.Item(0, ware.Id, position.Id, units, 0, 0));
                    }
                    else
                    {
                        
                    }
                }
            }            

            throw new NotImplementedException();
        }

        protected bool IsOrderValid(List<OrderCreatedIntegrationEvent.OrderItem> items)
        {
            bool valid = true;

            foreach (OrderCreatedIntegrationEvent.OrderItem item in items)
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
