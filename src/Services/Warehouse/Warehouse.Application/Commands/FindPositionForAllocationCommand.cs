using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindPositionForAllocationCommand : IRequest<Position>
    {
        public FindPositionForAllocationCommand(int warehouseId, int wareId, int count) : this(new FindPositionForAllocationCommandModel(warehouseId, wareId, count))
        {

        }
        public FindPositionForAllocationCommand(FindPositionForAllocationCommandModel model)
        {
            this.Model = model;
        }

        public FindPositionForAllocationCommandModel Model { get; }

        public class FindPositionForAllocationCommandModel
        {
            public FindPositionForAllocationCommandModel(int warehouseId, int wareId, int count)
            {
                this.WarehouseId = warehouseId;
                this.WareId = wareId;
                this.Count = count;
            }

            public int WarehouseId { get; }
            public int WareId { get; }
            public int Count { get; }
        }
    }
}
