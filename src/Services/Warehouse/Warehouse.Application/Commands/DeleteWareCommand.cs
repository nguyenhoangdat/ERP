using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteWareCommand : IRequest<Ware>
    {
        public DeleteWareCommand(DeleteWareCommandModel model)
        {
            this.Model = model;
        }
        public DeleteWareCommand(int wareId) : this(new DeleteWareCommandModel(wareId))
        {

        }

        public DeleteWareCommandModel Model { get; }

        public class DeleteWareCommandModel
        {
            public DeleteWareCommandModel(int wareId)
            {
                this.WareId = wareId;
            }

            public int WareId { get; }
        }
    }
}
