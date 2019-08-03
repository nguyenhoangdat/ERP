using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public UpdateWarehouseCommand(int id, string name, Address address)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
        }
        public UpdateWarehouseCommand(Domain.Entities.Warehouse warehouse)
            : this(warehouse.Id, warehouse.Name, warehouse.Address) { }

        public int Id { get; }
        public string Name { get; }
        public Address Address { get; }
    }
}
