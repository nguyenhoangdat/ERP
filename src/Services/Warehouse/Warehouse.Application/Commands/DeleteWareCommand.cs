using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteWareCommand : IRequest<Ware>
    {
        public DeleteWareCommand(int wareId)
        {
            this.WareId = wareId;
        }

        public int WareId { get; }
    }
}
