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
    public class UpdateWareCommandHandler : IRequestHandler<UpdateWareCommand, Ware>
    {
        public UpdateWareCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Ware> Handle(UpdateWareCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Wares.Any(x => x.Id == request.WareId))
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Ware_Update_EntityNotFoundException"], request.WareId));
            }

            Ware ware = this.DatabaseContext.Wares.FirstOrDefault(x => x.Id == request.WareId);
            ware.ProductName = request.ProductName;
            ware.Width = request.Width;
            ware.Height = request.Height;
            ware.Depth = request.Depth;
            ware.Weight = request.Weight;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new WareUpdatedDomainEvent(ware), cancellationToken);

            return ware;
        }
    }
}
