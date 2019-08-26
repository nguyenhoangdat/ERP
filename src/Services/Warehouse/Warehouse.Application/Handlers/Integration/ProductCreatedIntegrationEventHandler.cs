using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.Messaging;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductCreatedIntegrationEventHandler : IIntegrationEventHandler<ProductCreatedIntegrationEvent>
    {
        protected ILogger<ProductCreatedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductCreatedIntegrationEventHandler(ILogger<ProductCreatedIntegrationEventHandler> logger, IMediator mediator)
        {
            this.Logger = logger;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductCreatedIntegrationEvent @event)
        {
            try
            {
                await this.Mediator.Send(new CreateWareCommand(@event.ProductId, @event.ProductName));
            }
            catch (EntityAlreadyExitsException ex)
            {
                this.Logger.Log(LogLevel.Critical, ex.Message);
            }
        }
    }
}
