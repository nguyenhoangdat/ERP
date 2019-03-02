using MediatR;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductRemovedIntegrationEventHandler : IIntegrationEventHandler<ProductRemovedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public ProductRemovedIntegrationEventHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductRemovedIntegrationEvent @event)
        {
            Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == @event.ProductId).FirstOrDefault();
            ware = await this.Mediator.Send(new DeleteWareCommand(ware.Id));

            await this.Mediator.Publish(new WareDeletedDomainEvent(ware));
        }
    }
}
