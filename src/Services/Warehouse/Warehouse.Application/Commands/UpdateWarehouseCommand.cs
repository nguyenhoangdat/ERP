using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public UpdateWarehouseCommand(int id, string name, Address address)
        {
            if (id <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

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
