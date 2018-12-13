using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public CreateWarehouseCommand(CreateWarehouseCommandModel model)
        {
            this.Model = model;
        }

        public CreateWarehouseCommandModel Model { get; }

        public class CreateWarehouseCommandModel
        {
            public CreateWarehouseCommandModel(string name, Address address)
            {
                this.Name = name;
                this.Address = address;
            }

            public string Name { get; }
            public Address Address { get; }
        }
    }
}
