using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class GetWareAvailabilityCommandHandler : IRequestHandler<GetWareAvailabilityCommand, IEnumerable<WareAvailability>>
    {
        public GetWareAvailabilityCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<WareAvailability>> Handle(GetWareAvailabilityCommand request, CancellationToken cancellationToken)
        {
            Ware ware = await this.DatabaseContext.Wares.FindAsync(new object[] { request.WareId }, cancellationToken);
            if (ware == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Ware_EntityNotFoundException"], request.WareId));
            }

            return this.DatabaseContext.Positions.Where(x => x.GetWare().Id == request.WareId).Select(x => new WareAvailability()
            {
                Ware = x.GetWare(),
                Warehouse = x.Section.Warehouse,
                UnitsAvailable = x.CountWare()
            });
        }
    }
}
