using MediatR;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveWarehouseToBinCommand : IRequest<Domain.Entities.Warehouse>
    {
        public MoveWarehouseToBinCommand(int warehouseId, bool movedToBinInCascade)
        {
            if (warehouseId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(warehouseId));
            }

            this.WarehouseId = warehouseId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public int WarehouseId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
