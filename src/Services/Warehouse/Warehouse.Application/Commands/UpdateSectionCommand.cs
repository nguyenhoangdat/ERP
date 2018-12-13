using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateSectionCommand : IRequest<Section>
    {
        public UpdateSectionCommand(UpdateSectionCommandModel model)
        {
            this.Model = model;
        }

        public UpdateSectionCommandModel Model { get; }

        public class UpdateSectionCommandModel
        {
            public UpdateSectionCommandModel(int id, string name, int warehouseId)
            {
                this.Id = id;
                this.Name = name;
                this.WarehouseId = warehouseId;
            }
            public UpdateSectionCommandModel(int id, string name, Domain.Entities.Warehouse warehouse) : this(id, name, warehouse.Id)
            {

            }

            public int Id { get; }
            public string Name { get; }
            public int WarehouseId { get; }
        }
    }
}
