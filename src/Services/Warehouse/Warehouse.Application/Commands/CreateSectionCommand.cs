using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateSectionCommand : IRequest<Section>
    {
        public CreateSectionCommand(string name, int warehouseId)
        {
            this.Name = name;
            this.WarehouseId = warehouseId;
        }
        public CreateSectionCommand(string name, Domain.Entities.Warehouse warehouse) : this(name, warehouse.Id) { }

        public string Name { get; }
        public int WarehouseId { get; }
    }
}
