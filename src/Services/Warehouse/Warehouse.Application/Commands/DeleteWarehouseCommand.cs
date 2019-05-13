using MediatR;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteWarehouseCommand : IRequest<Domain.Entities.Warehouse>
    {
        public DeleteWarehouseCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; }
    }
}
