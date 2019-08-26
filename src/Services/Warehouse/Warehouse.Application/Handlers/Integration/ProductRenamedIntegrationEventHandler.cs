using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.Messaging;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductRenamedIntegrationEventHandler : IIntegrationEventHandler<ProductRenamedIntegrationEvent>
    {
        protected ILogger<ProductRenamedIntegrationEvent> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductRenamedIntegrationEventHandler(ILogger<ProductRenamedIntegrationEvent> logger, IMediator mediator)
        {
            this.Logger = logger;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductRenamedIntegrationEvent @event)
        {
            try
            {
                Ware ware = await this.Mediator.Send(new RenameWareCommand(@event.ProductId, @event.ProductName));
                await this.Mediator.Publish(new WareRenamedDomainEvent(ware));
            }
            catch (EntityNotFoundException ex)
            {
                this.Logger.Log(LogLevel.Critical, ex.Message);
                await this.Mediator.Publish(new WareRenameFailedDomainEvent(@event.ProductId));
            }
        }
    }
}
