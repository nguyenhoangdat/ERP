using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindPositionByIdCommand : IRequest<Position>
    {
        public FindPositionByIdCommand(long id)
        {
            this.Id = id;
        }

        public long Id { get; }
    }
}
