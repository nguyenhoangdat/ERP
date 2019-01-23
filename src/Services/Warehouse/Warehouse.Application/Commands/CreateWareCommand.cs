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
        public CreateWareCommand(int productId, string productName) : this(new CreateWareCommandModel(productId, productName))
        {

        }

        public CreateWareCommandModel Model { get; }

        public class CreateWareCommandModel
        {
            public CreateWareCommandModel(int productId, string productName)
            {
                this.ProductId = productId;
                this.ProductName = productName;
            }

            public int ProductId { get; }
            public string ProductName { get; }
        }
    }
}
