using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public CreateWarehouseCommand(CreateWarehouseCommandModel model)
        {
            this.Model = model;
        }
        public CreateWarehouseCommand(Domain.Entities.Warehouse warehouse)
            : this(new CreateWarehouseCommandModel(warehouse.Name, warehouse.Address, warehouse.Sections))
        {
        }

        public CreateWarehouseCommandModel Model { get; }

        public class CreateWarehouseCommandModel
        {
            public CreateWarehouseCommandModel(string name, Address address, ICollection<Section> sections)
            {
                this.Name = name;
                this.Address = address;
                this.Sections = sections;
            }

            public string Name { get; }
            public Address Address { get; }
            public ICollection<Section> Sections { get; }
        }
    }
}
