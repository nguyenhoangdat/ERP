using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.Messaging;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductRemovedIntegrationEventHandler : IIntegrationEventHandler<ProductRemovedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected ILogger<ProductRemovedIntegrationEventHandler> Logger { get; }
        protected IMediator Mediator { get; }

        public ProductRemovedIntegrationEventHandler(DatabaseContext context, ILogger<ProductRemovedIntegrationEventHandler> logger, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Logger = logger;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductRemovedIntegrationEvent @event)
        {
            Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == @event.ProductId).FirstOrDefault();
            if (ware == null)
            {
                this.Logger.Log(LogLevel.Critical, Properties.Resources.Ware_ProductId_EntityNotFoundException, @event.ProductId);
            }
            else
            {
                ware = await this.Mediator.Send(new DeleteWareCommand(ware.Id));
            }
        }
    }
}
