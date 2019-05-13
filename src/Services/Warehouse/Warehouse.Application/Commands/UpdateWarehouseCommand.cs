using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public UpdateWarehouseCommand(int id, string name, Address address, ICollection<Section> sections)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.Sections = sections;
        }
        public UpdateWarehouseCommand(Domain.Entities.Warehouse warehouse)
            : this(warehouse.Id, warehouse.Name, warehouse.Address, warehouse.Sections) { }

        public int Id { get; }
        public string Name { get; }
        public Address Address { get; }
        public ICollection<Section> Sections { get; }
    }
}
