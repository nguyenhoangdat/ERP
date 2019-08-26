using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateWareCommandHandler : IRequestHandler<CreateWareCommand, Ware>
    {
        public CreateWareCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Ware> Handle(CreateWareCommand request, CancellationToken cancellationToken)
        {
            if (this.DatabaseContext.Wares.Any(x => x.ProductId == request.ProductId))
            {
                throw new EntityAlreadyExitsException(string.Format(Properties.Resources.Ware_EntityAlreadyExitsException, request.ProductId));
            }

            Ware ware = this.DatabaseContext.Wares.Add(new Ware(request.ProductId, request.ProductName)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new WareCreatedDomainEvent(ware), cancellationToken);

            return ware;
        }
    }
}
