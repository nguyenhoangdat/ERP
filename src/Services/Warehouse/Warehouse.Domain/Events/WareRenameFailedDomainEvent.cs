using MediatR;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class WareRenameFailedDomainEvent : INotification
    {
        public WareRenameFailedDomainEvent(int productId)
        {
            this.ProductId = productId;
        }

        public int ProductId { get; }
    }
}
