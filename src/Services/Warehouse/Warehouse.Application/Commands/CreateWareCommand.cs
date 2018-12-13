using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateWareCommand : IRequest<Ware>
    {
        public CreateWareCommand(CreateWareCommandModel model)
        {
            this.Model = model;
        }

        public CreateWareCommandModel Model { get; }

        public class CreateWareCommandModel
        {
            public int ProductId { get; }
            public string ProductName { get; }
        }
    }
}
