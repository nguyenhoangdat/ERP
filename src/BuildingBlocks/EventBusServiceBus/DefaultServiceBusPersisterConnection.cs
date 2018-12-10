using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using System;

namespace Restmium.ERP.BuildingBlocks.EventBusServiceBus
{
    public class DefaultServiceBusPersisterConnection : IServiceBusPersistentConnection
    {
        private readonly ILogger<DefaultServiceBusPersisterConnection> _logger;
        private ITopicClient _topicClient;

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        public DefaultServiceBusPersisterConnection(
            ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder,
            ILogger<DefaultServiceBusPersisterConnection> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ServiceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ?? throw new ArgumentNullException(nameof(serviceBusConnectionStringBuilder));
            _topicClient = new TopicClient(ServiceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ITopicClient GetClient()
        {
            if (_topicClient.IsClosedOrClosing)
            {
                _topicClient = new TopicClient(ServiceBusConnectionStringBuilder, RetryPolicy.Default);
            }

            return _topicClient;
        }
    }
}
