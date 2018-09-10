using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Restmium.ERP.BuildingBlocks.EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, List<Type>> _typesOfHandlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _typesOfHandlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_typesOfHandlers.Keys.Any();
        public void Clear() => _typesOfHandlers.Clear();        

        public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            string eventName = GetEventKey<T>();
            Type handlerType = typeof(TH);

            if (!HasSubscriptionsForEvent(eventName))
            {
                _typesOfHandlers.Add(eventName, new List<Type>());
            }

            if (_typesOfHandlers[eventName].Contains(handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _typesOfHandlers[eventName].Add(handlerType);
            _eventTypes.Add(typeof(T));
        }

        #region RemoveSubscription
        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = GetEventKey<T>();
            Type handlerTypeToRemove = FindHandlerTypeToRemove(eventName, typeof(TH));

            if (handlerTypeToRemove != null)
            {
                _typesOfHandlers[eventName].Remove(handlerTypeToRemove);

                if (!_typesOfHandlers[eventName].Any())
                {
                    _typesOfHandlers.Remove(eventName);
                    Type eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }
            }
        }
        private Type FindHandlerTypeToRemove(string eventName, Type handlerType)
        {
            return (!HasSubscriptionsForEvent(eventName)) ? null : _typesOfHandlers[eventName].SingleOrDefault(s => s == handlerType);
        }
        private void RaiseOnEventRemoved(string eventName)
        {
            OnEventRemoved?.Invoke(this, eventName);
        }
        #endregion

        public string GetEventKey<T>() => typeof(T).Name;
        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(x => x.Name == eventName);

        public IEnumerable<Type> GetTypesOfHandlersForEvent(string eventName) => this._typesOfHandlers[eventName];

        public bool HasSubscriptionsForEvent(string eventName) => _typesOfHandlers.ContainsKey(eventName);
    }
}
