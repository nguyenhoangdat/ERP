using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeletePositionCommand : IRequest<Position>
    {
        public DeletePositionCommand(long id)
        {
            this.Id = id;
        }

        public long Id { get; }
    }
}
