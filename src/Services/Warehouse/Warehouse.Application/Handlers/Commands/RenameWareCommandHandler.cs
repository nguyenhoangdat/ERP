using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RenameWareCommandHandler : IRequestHandler<RenameWareCommand, Ware>
    {
        public RenameWareCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        public DatabaseContext DatabaseContext { get; }
        public IMediator Mediator { get; }

        public async Task<Ware> Handle(RenameWareCommand request, CancellationToken cancellationToken)
        {
            Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == request.ProductId).FirstOrDefault();

            if (ware == null)
            {
                await this.Mediator.Publish(new WareRenameFailedDomainEvent(request.ProductId));
            }
            else
            {
                ware.ProductName = request.Name;
                await this.DatabaseContext.SaveChangesAsync(cancellationToken);

                await this.Mediator.Publish(new WareRenamedDomainEvent(ware));
            }

            return ware;
        }
    }
}
