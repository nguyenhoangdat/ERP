using MediatR;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWarehouseByIdCommand : IRequest<Domain.Entities.Warehouse>
    {
        public FindWarehouseByIdCommand(FindWarehouseByIdCommandModel model)
        {
            this.Model = model;
        }
        public FindWarehouseByIdCommand(int id) : this(new FindWarehouseByIdCommandModel(id))
        {
        }

        public FindWarehouseByIdCommandModel Model { get; }

        public class FindWarehouseByIdCommandModel
        {
            public FindWarehouseByIdCommandModel(int id)
            {
                this.Id = id;
            }

            public int Id { get; }
        }
    }
}
