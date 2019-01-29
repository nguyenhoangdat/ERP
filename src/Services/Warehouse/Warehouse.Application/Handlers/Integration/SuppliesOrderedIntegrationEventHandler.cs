using MediatR;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Integration
{
    public class SuppliesOrderedIntegrationEventHandler : IIntegrationEventHandler<SuppliesOrderedIntegrationEvent>
    {
        public SuppliesOrderedIntegrationEventHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task Handle(SuppliesOrderedIntegrationEvent @event)
        {
            //TODO: Create Receipt

            // WareId, Count
            // DO NOT ASSIGN POSITIONS!!! - This need to be processed REAL-TIME!!!

            throw new System.NotImplementedException();
        }
    }
}
