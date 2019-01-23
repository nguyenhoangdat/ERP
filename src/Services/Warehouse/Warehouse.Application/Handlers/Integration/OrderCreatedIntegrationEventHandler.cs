using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Application.Commands;
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
        protected ILogger<OrderCreatedIntegrationEventHandler> Logger { get; }     
        protected IMediator Mediator { get; }

        public OrderCreatedIntegrationEventHandler(
            DatabaseContext context,
            ILogger<OrderCreatedIntegrationEventHandler> logger,
            IMediator mediator)
        {
            this.DatabaseContext = context;
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

            // Create Model.Items
            List<CreateIssueSlipCommand.CreateIssueSlipCommandModel.Item> modelItems = new List<CreateIssueSlipCommand.CreateIssueSlipCommandModel.Item>(@event.OrderItems.Count);
            foreach (OrderCreatedIntegrationEvent.OrderItem item in @event.OrderItems)
            {
                Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                modelItems.Add(new CreateIssueSlipCommand.CreateIssueSlipCommandModel.Item(ware, item.Units));
            }

            // Create CommandModel
            string name = ""; //TODO: Add IssueSlip name
            CreateIssueSlipCommand.CreateIssueSlipCommandModel commandModel = new CreateIssueSlipCommand.CreateIssueSlipCommandModel(name, @event.OrderId, @event.UtcDispatchDate, @event.UtcDeliveryDate, modelItems);

            // Create IssueSlip
            IssueSlip issueSlip = await this.Mediator.Send(new CreateIssueSlipCommand(commandModel));
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
