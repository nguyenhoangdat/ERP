using Restmium.ERP.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Restmium.ERP.BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}