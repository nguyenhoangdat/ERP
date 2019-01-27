using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWareAvailabilityCommand : IRequest<IEnumerable<WareAvailabilityDTO>>
    {
        public GetWareAvailabilityCommand(GetWareAvailabilityCommandModel model)
        {
            this.Model = model;
        }
        public GetWareAvailabilityCommand(int wareId) 
            : this(new GetWareAvailabilityCommandModel(wareId)) { }

        public GetWareAvailabilityCommandModel Model { get; }

        public class GetWareAvailabilityCommandModel
        {
            public GetWareAvailabilityCommandModel(int wareId)
            {
                this.WareId = wareId;
            }

            public int WareId { get; }
        }
    }
}
