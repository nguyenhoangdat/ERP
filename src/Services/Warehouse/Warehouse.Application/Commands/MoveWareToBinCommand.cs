using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveWareToBinCommand : IRequest<Ware>
    {
        public MoveWareToBinCommand(int wareId, bool movedToBinInCascade)
        {
            this.WareId = wareId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public int WareId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
