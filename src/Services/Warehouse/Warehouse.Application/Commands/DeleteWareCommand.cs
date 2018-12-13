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

        public DeleteWareCommandModel Model { get; }

        public class DeleteWareCommandModel
        {
            public int Id { get; }
        }
    }
}
