using MediatR;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class ProductRenamedIntegrationEventHandler : IIntegrationEventHandler<ProductRenamedIntegrationEvent>
    {
        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public ProductRenamedIntegrationEventHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        public async Task Handle(ProductRenamedIntegrationEvent @event)
        {
            Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == @event.ProductId).FirstOrDefault();
            await this.Mediator.Send(new UpdateWareCommand(ware.Id, @event.ProductName, ware.Width, ware.Height, ware.Depth, ware.Weight));

            await this.Mediator.Publish(new WareRenamedDomainEvent(ware));
        }
    }
}
