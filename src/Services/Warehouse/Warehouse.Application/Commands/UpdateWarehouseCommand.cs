using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public UpdateWarehouseCommand(UpdateWarehouseCommandModel model)
        {
            this.Model = model;
        }

        public UpdateWarehouseCommandModel Model { get; }

        public class UpdateWarehouseCommandModel
        {
            public UpdateWarehouseCommandModel(int id, string name, Address address)
            {
                this.Id = id;
                this.Name = name;
                this.Address = address;
            }

            public int Id { get; }
            public string Name { get; }
            public Address Address { get; }
        }
    }
}
