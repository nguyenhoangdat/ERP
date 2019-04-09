using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RenameWareCommand : IRequest<Ware>
    {
        public RenameWareCommand(int productId, string name)
        {
            this.ProductId = productId;
            this.Name = name;
        }

        public int ProductId { get; }
        public string Name { get; }
    }
}
