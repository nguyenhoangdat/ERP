using MediatR;
using Restmium.ERP.Integration.Warehouse;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RejectOrderCommandHandler : IRequestHandler<RejectOrderCommand, bool>
    {
        public RejectOrderCommandHandler(IEventBus eventBus)
        {
            this.EventBus = eventBus;
        }

        protected IEventBus EventBus { get; }

        public async Task<bool> Handle(RejectOrderCommand request, CancellationToken cancellationToken)
        {
            if (this.EventBus != null)
            {
                this.EventBus.Publish(new OrderWarehouseRejectedIntegrationEvent(request.OrderId));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
