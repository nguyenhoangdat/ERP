using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteWareCommand : IRequest<Ware>
    {
        public DeleteWareCommand(DeleteWareCommandModel ware)
        {
            this.Ware = ware;
        }

        public DeleteWareCommandModel Ware { get; }

        public class DeleteWareCommandModel
        {
            public int Id { get; }
        }
    }
}
