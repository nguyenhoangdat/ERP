using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWaresInWarehouseCommand : IRequest<IEnumerable<Ware>>
    {
        public FindWaresInWarehouseCommand(FindWaresInWarehouseCommandModel model)
        {
            this.Model = model;
        }
        public FindWaresInWarehouseCommand(int warehouseId)
            : this(new FindWaresInWarehouseCommandModel(warehouseId)) { }

        public FindWaresInWarehouseCommandModel Model { get; }

        public class FindWaresInWarehouseCommandModel
        {
            public FindWaresInWarehouseCommandModel(int warehouseId)
            {
                this.WarehouseId = warehouseId;
            }

            public int WarehouseId { get; }
        }
    }
}
