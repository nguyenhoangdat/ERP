using MediatR;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWarehouseByIdCommand : IRequest<Domain.Entities.Warehouse>
    {
        public FindWarehouseByIdCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; }
    }
}
