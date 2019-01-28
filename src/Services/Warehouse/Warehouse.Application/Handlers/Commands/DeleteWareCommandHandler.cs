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
    public class DeleteWareCommandHandler : IRequestHandler<DeleteWareCommand, Ware>
    {
        public DeleteWareCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Ware> Handle(DeleteWareCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Wares.Any(x => x.Id == request.Model.WareId))
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Ware_Delete_EntityNotFoundException"], request.Model.WareId));
            }

            Ware ware = this.DatabaseContext.Wares.Remove(this.DatabaseContext.Wares.Find(request.Model.WareId)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new WareDeletedDomainEvent(ware), cancellationToken);

            return ware;
        }
    }
}
