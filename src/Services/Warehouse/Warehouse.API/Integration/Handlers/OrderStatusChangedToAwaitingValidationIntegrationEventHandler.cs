using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
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

                IssueSlip issueSlip = new IssueSlip
                {
                    Items = new List<IssueSlip.Item>()
                };

                IssueSlipCreatedIntegrationEvent issueSlipCreatedIntegrationEvent = new IssueSlipCreatedIntegrationEvent
                {
                    OrderId = @event.OrderId,
                    Items = new List<IssueSlipCreatedIntegrationEvent.IssueSlipItem>()

                    //TODO: Dates
                };

                foreach (OrderStatusChangedToAwaitingValidationIntegrationEvent.OrderItem item in @event.OrderItems)
                {
                    Ware ware = _databaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                    int orderUnits = item.Units;

                    foreach (Position position in _databaseContext.Positions.Where(x => x.WareId == ware.Id).OrderBy(x => x.Rating).ThenByDescending(x => x.Count()))
                    {
                        int positionCount = position.Count();

                        IssueSlip.Item issueSlipItem = new IssueSlip.Item
                        {
                            IssueSlipId = 0,
                            IssueSlip = issueSlip,
                            WareId = ware.Id,
                            Ware = ware,
                            RequestedUnits = (positionCount < item.Units) ? positionCount : item.Units,
                            IssuedUnits = 0, //IssueSlip wasn't processed yet,
                            PositionId = position.Id,
                            Position = position
                        };
                        issueSlip.Items.Add(issueSlipItem);

                        item.Units -= issueSlipItem.RequestedUnits;
                        if (item.Units == 0)
                        {
                            issueSlipCreatedIntegrationEvent.Items.Add(new IssueSlipCreatedIntegrationEvent.IssueSlipItem()
                            {
                                ProductId = ware.ProductId,
                                Units = orderUnits,
                                Width = ware.Width,
                                Height = ware.Height,
                                Depth = ware.Depth,

                                Weight = ware.Weight
                            });

                            break;
                        }
                    }
                }

                
                //TODO: Publish the IssueSlipCreatedIntegrationEvent
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
