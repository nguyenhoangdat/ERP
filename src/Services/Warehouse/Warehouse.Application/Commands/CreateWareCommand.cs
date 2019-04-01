using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateWareCommand : IRequest<Ware>
    {
        public CreateWareCommand(int productId, string productName)
        {
            this.ProductId = productId;
            this.ProductName = productName;
        }

        public int ProductId { get; }
        public string ProductName { get; }
    }
}
