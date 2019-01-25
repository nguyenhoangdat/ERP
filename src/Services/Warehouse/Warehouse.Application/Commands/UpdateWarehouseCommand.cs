using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public UpdateWarehouseCommand(UpdateWarehouseCommandModel model)
        {
            this.Model = model;
        }
        public UpdateWarehouseCommand(Domain.Entities.Warehouse warehouse)
            : this(new UpdateWarehouseCommandModel(warehouse.Id, warehouse.Name, warehouse.Address, warehouse.Sections))
        {
        }

        public UpdateWarehouseCommandModel Model { get; }

        public class UpdateWarehouseCommandModel
        {
            public UpdateWarehouseCommandModel(int id, string name, Address address, ICollection<Section> sections)
            {
                this.Id = id;
                this.Name = name;
                this.Address = address;
                this.Sections = sections;
            }

            public int Id { get; }
            public string Name { get; }
            public Address Address { get; }
            public ICollection<Section> Sections { get; }
        }
    }
}
