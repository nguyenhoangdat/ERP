using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.Integration.Ordering;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors;
using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Extensions;
using Restmium.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<OrderCreatedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }
        protected IIssueSlipPositionSelector PositionSelector { get; }

        public OrderCreatedIntegrationEventHandler(DatabaseContext context, ILogger<OrderCreatedIntegrationEventHandler> logger, IMediator mediator, IIssueSlipPositionSelector positionSelector)
        {
            this.DatabaseContext = context;
            this.Logger = logger;
            this.Mediator = mediator;
            this.PositionSelector = positionSelector;
        }

        /// <summary>
        /// Handles OrderCreatedIntegrationEvent and creates new IssueSlip
        /// </summary>
        /// <param name="event">The instance of OrderCreatedIntegrationEvent</param>
        /// <returns></returns>
        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            bool isOrderValid = await this.Mediator.Send(new ValidateOrderCommand(this.ConvertToProductCounts(@event.OrderItems)));

            if (isOrderValid)
            {
                await this.Mediator.Send(new CreateIssueSlipCommand(@event.OrderId, @event.UtcDispatchDate, @event.UtcDeliveryDate, this.GetItems(@event.OrderItems)));
            }
            else
            {
                await this.Mediator.Send(new RejectOrderCommand(@event.OrderId));
            }
        }

        protected IEnumerable<ValidateOrderCommand.ProductCount> ConvertToProductCounts(IEnumerable<OrderCreatedIntegrationEvent.OrderItem> orderItems)
        {
            List<ValidateOrderCommand.ProductCount> productCounts = new List<ValidateOrderCommand.ProductCount>(orderItems.Count());
            foreach (OrderCreatedIntegrationEvent.OrderItem item in orderItems)
            {
                productCounts.Add(new ValidateOrderCommand.ProductCount(item.ProductId, item.Units));
            }
            return productCounts;
        }
        protected IEnumerable<CreateIssueSlipCommand.Item> GetItems(IEnumerable<OrderCreatedIntegrationEvent.OrderItem> orderItems)
        {
            LinkedList<CreateIssueSlipCommand.Item> modelItems = new LinkedList<CreateIssueSlipCommand.Item>();
            foreach (OrderCreatedIntegrationEvent.OrderItem item in orderItems)
            {
                Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (ware == null)
                {
                    this.Logger.Log(LogLevel.Critical, Properties.Resources.Ware_ProductId_EntityNotFoundException, item.ProductId);
                    continue;
                }

                IEnumerable<PositionCount> positionCounts = this.PositionSelector?.GetPositions(ware, item.Units);

                // Positions will be selected manually
                if (positionCounts == null)
                {
                    modelItems.AddLast(new CreateIssueSlipCommand.Item(ware.Id, null, item.Units));
                }
                else
                {
                    foreach (PositionCount positionCount in positionCounts)
                    {
                        modelItems.AddLast(new CreateIssueSlipCommand.Item(ware.Id, positionCount.Position.Id, positionCount.Count));
                    }
                }
            }

            return modelItems;
        }
    }
}
