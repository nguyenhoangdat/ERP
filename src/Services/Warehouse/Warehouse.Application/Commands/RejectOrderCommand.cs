using MediatR;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RejectOrderCommand : IRequest<bool>
    {
        public RejectOrderCommand(long orderId)
        {
            this.OrderId = orderId;
        }

        public long OrderId { get; }
    }
}
