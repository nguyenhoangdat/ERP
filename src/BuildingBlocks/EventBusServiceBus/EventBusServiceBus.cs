using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Restmium.ERP.BuildingBlocks.EventBusServiceBus
{
    public sealed class EventBusServiceBus : IEventBus
    {
        private readonly IServiceBusPersistentConnection _eventBusPersistentConnection;
        private readonly ILogger<EventBusServiceBus> _logger;

        private readonly IEventBusSubscriptionsManager _subscriptionsManager;
        private readonly SubscriptionClient _subscriptionClient;

        private const string INTEGRATION_EVENT_SUFIX = "IntegrationEvent";

        public EventBusServiceBus(
            IServiceBusPersistentConnection persistentConnection,
            ILogger<EventBusServiceBus> logger,
            IEventBusSubscriptionsManager subscriptionsManager,
            string subscriptionClientName)
        {
            _eventBusPersistentConnection = persistentConnection;
            _logger = logger;
            _subscriptionsManager = subscriptionsManager;
            _subscriptionClient = new SubscriptionClient(persistentConnection.ServiceBusConnectionStringBuilder, subscriptionClientName);

            this.RemoveDefaultRule();
            this.RegisterSubscriptionClientMessageHandler();
        }

        #region IEventBus
        public void Publish(IntegrationEvent @event)
        {
            string eventName = @event.GetType().Name.Replace(INTEGRATION_EVENT_SUFIX, "");
            string jsonMessage = JsonConvert.SerializeObject(@event);
            byte[] body = Encoding.UTF8.GetBytes(jsonMessage);

            Message message = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
                Label = eventName
            };

            ITopicClient client = _eventBusPersistentConnection.GetClient();

            client.SendAsync(message)
                .GetAwaiter()
                .GetResult();
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFIX, "");

            bool containsKey = _subscriptionsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                try
                {
                    _subscriptionClient.AddRuleAsync(new RuleDescription()
                    {
                        Filter = new CorrelationFilter() { Label = eventName },
                        Name = eventName
                    }).GetAwaiter().GetResult();
                }
                catch (ServiceBusException)
                {
                    _logger.LogInformation($"The messaging entity {eventName} already exists.");
                }
            }

            _subscriptionsManager.AddSubscription<T, TH>();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFIX, "");

            try
            {
                _subscriptionClient
                    .RemoveRuleAsync(eventName)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity {eventName} Could not be found.");
            }

            _subscriptionsManager.RemoveSubscription<T, TH>();
        }

        #endregion

        #region SubscriptionClient's methods
        private void RegisterSubscriptionClientMessageHandler()
        {
            _subscriptionClient.RegisterMessageHandler(
                async (message, token) =>
                {
                    string eventName = $"{message.Label}{INTEGRATION_EVENT_SUFIX}";
                    string messageData = Encoding.UTF8.GetString(message.Body);
                    await ProcessEvent(eventName, messageData);

                    // Complete the message so that it is not received again.
                    await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                },
                new MessageHandlerOptions(ExceptionReceivedHandler) { MaxConcurrentCalls = 10, AutoComplete = false });
        }
        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                foreach (IIntegrationEventHandler<IntegrationEvent> handler in _subscriptionsManager.GetHandlersForEvent(eventName))
                {
                    Type eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                    object integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                    await handler.Handle(integrationEvent as IntegrationEvent);
                }
            }

            throw new NotImplementedException();
        } //TODO: Debug
        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
        private void RemoveDefaultRule()
        {
            try
            {
                _subscriptionClient
                 .RemoveRuleAsync(RuleDescription.DefaultRuleName)
                 .GetAwaiter()
                 .GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity { RuleDescription.DefaultRuleName } Could not be found.");
            }
        }
        #endregion
    }
}
