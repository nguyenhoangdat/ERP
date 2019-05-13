using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWareAvailabilityCommand : IRequest<IEnumerable<WareAvailabilityDTO>>
    {
        public GetWareAvailabilityCommand(int wareId)
        {
            this.WareId = wareId;
        }

        public int WareId { get; }
    }
}
