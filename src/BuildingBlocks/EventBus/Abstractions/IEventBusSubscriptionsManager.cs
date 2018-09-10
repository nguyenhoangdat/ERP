using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.BuildingBlocks.EventBus.Abstractions
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();

        IEnumerable<Type> GetTypesOfHandlersForEvent(string eventName);

        string GetEventKey<T>();
    }
}
