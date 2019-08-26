using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.Integration.Supply;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors;
using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class SuppliesOrderedIntegrationEventHandler : IIntegrationEventHandler<SuppliesOrderedIntegrationEvent>
    {
        public SuppliesOrderedIntegrationEventHandler(DatabaseContext databaseContext, ILogger<SuppliesOrderedIntegrationEventHandler> logger, IMediator mediator, IReceiptPositionSelector positionSelector)
        {
            this.DatabaseContext = databaseContext;
            this.Logger = logger;
            this.Mediator = mediator;
            this.PositionSelector = positionSelector;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<SuppliesOrderedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }
        protected IReceiptPositionSelector PositionSelector { get; }

        public async Task Handle(SuppliesOrderedIntegrationEvent @event)
        {
            // Create Receipt
            Receipt receipt = await this.Mediator.Send(new CreateReceiptCommand(@event.UtcExpected, this.GetItems(@event.Items)));

            // Publish DomainEven that the Receipt has been created
            await this.Mediator.Publish(new ReceiptCreatedDomainEvent(receipt));
        }

        protected IEnumerable<CreateReceiptCommand.Item> GetItems(IEnumerable<SuppliesOrderedIntegrationEvent.SupplyItem> eventItems)
        {
            LinkedList<CreateReceiptCommand.Item> items = new LinkedList<CreateReceiptCommand.Item>();
            foreach (SuppliesOrderedIntegrationEvent.SupplyItem item in eventItems)
            {
                Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (ware == null)
                {
                    this.Logger.Log(LogLevel.Critical, Properties.Resources.Ware_ProductId_EntityNotFoundException, item.ProductId);
                    continue;
                }

                IEnumerable<PositionCount> positionCounts = this.PositionSelector?.GetPositions(ware, item.Count);

                // Positions will be selected manually
                if (positionCounts == null)
                {
                    items.AddLast(new CreateReceiptCommand.Item(ware.Id, null, item.Count));
                }
                else
                {
                    foreach (PositionCount positionCount in positionCounts)
                    {
                        items.AddLast(new CreateReceiptCommand.Item(ware.Id, positionCount.Position.Id, positionCount.Count));
                    }
                }
            }

            return items;
        }
    }
}
