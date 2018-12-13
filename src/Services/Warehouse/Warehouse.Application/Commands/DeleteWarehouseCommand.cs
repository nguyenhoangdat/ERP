using MediatR;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public DeleteWarehouseCommand(DeleteWarehouseCommandModel model)
        {
            this.Model = model;
        }

        public DeleteWarehouseCommandModel Model { get; }

        public class DeleteWarehouseCommandModel
        {
            public DeleteWarehouseCommandModel(int id)
            {
                this.Id = id;
            }

            public int Id { get; }
        }
    }
}
