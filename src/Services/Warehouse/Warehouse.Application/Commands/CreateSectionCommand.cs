using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateSectionCommand : IRequest<Section>
    {
        public CreateSectionCommand(CreateSectionCommandModel model)
        {
            this.Model = model;
        }
        public CreateSectionCommand(string name, int warehouseId)
            : this(new CreateSectionCommandModel(name, warehouseId)) { }

        public CreateSectionCommandModel Model { get; }

        public class CreateSectionCommandModel
        {
            public CreateSectionCommandModel(string name, int warehouseId)
            {
                this.Name = name;
                this.WarehouseId = warehouseId;
            }
            public CreateSectionCommandModel(string name, Domain.Entities.Warehouse warehouse) : this(name, warehouse.Id)
            {

            }

            public string Name { get; }
            public int WarehouseId { get; }
        }
    }
}
