using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveWareToBinCommand : IRequest<Ware>
    {
        public MoveWareToBinCommand(int wareId)
        {
            this.WareId = wareId;
        }

        public int WareId { get; }
    }
}
