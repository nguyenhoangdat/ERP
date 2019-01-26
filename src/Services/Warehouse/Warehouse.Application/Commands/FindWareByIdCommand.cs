using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWareByIdCommand : IRequest<Ware>
    {
        public FindWareByIdCommand(FindWareByIdCommandModel model)
        {
            this.Model = model;
        }
        public FindWareByIdCommand(int wareId)
            : this(new FindWareByIdCommandModel(wareId)) { }

        public FindWareByIdCommandModel Model { get; }

        public class FindWareByIdCommandModel
        {
            public FindWareByIdCommandModel(int wareId)
            {
                this.WareId = wareId;
            }

            public int WareId { get; }
        }
    }
}
