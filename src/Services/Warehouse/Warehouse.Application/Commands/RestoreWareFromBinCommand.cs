using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreWareFromBinCommand : IRequest<Ware>
    {
        public RestoreWareFromBinCommand(int wareId)
        {
            this.WareId = wareId;
        }

        public int WareId { get; }
    }
}
