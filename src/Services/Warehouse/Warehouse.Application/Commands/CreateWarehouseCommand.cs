using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public CreateWarehouseCommand(string name, Address address, ICollection<Section> sections)
        {
            this.Name = name;
            this.Address = address;
            this.Sections = sections;
        }
        public CreateWarehouseCommand(Domain.Entities.Warehouse warehouse) : this(warehouse.Name, warehouse.Address, warehouse.Sections)
        {
        }

        public string Name { get; }
        public Address Address { get; }
        public ICollection<Section> Sections { get; }
    }
}
