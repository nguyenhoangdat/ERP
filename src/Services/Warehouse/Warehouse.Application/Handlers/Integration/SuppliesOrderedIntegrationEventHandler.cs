using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Integration.Supply;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class SuppliesOrderedIntegrationEventHandler : IIntegrationEventHandler<SuppliesOrderedIntegrationEvent>
    {
        public SuppliesOrderedIntegrationEventHandler(DatabaseContext databaseContext, ILogger<SuppliesOrderedIntegrationEventHandler> logger, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Logger = logger;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        ILogger<SuppliesOrderedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(SuppliesOrderedIntegrationEvent @event)
        {
            // Create Receipt.Items without assigning positions
            List<CreateReceiptCommand.CreateReceiptCommandModel.Item> items = new List<CreateReceiptCommand.CreateReceiptCommandModel.Item>();
            foreach (SuppliesOrderedIntegrationEvent.SupplyItem item in @event.Items)
            {
                Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                if (ware == null)
                {
                    this.Logger.Log(LogLevel.Critical, string.Format(Resources.Exceptions.Values["ReceiptItem_Create_Ware_EntityNotFoundException"], item.ProductId, item.Count));
                }
                else
                {
                    items.Add(new CreateReceiptCommand.CreateReceiptCommandModel.Item(ware.Id, item.Count));
                }
            }

            // Create Receipt
            Receipt receipt = await this.Mediator.Send(new CreateReceiptCommand(@event.UtcExpected, items));

            // Publish DomainEven that the Receipt has been created
            await this.Mediator.Publish(new ReceiptCreatedDomainEvent(receipt));
        }
    }
}
