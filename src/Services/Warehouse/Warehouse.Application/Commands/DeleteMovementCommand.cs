using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteMovementCommand : IRequest<Movement>
    {
        public DeleteMovementCommand(long id)
        {
            this.Id = id;
        }

        public long Id { get; }
    }
}
