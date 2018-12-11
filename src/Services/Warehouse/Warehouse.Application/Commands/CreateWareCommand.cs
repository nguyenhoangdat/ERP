using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateWareCommand : IRequest<Ware>
    {
        public CreateWareCommand(CreateWareCommandModel ware)
        {
            this.Ware = ware;
        }

        public CreateWareCommandModel Ware { get; }

        public class CreateWareCommandModel
        {
            public int ProductId { get; }
            public string ProductName { get; }
        }
    }
}
