using Microsoft.Azure.ServiceBus;

namespace Restmium.ERP.BuildingBlocks.EventBusServiceBus
{
    public interface IServiceBusPersistentConnection
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient GetClient();
    }
}