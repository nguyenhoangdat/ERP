using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class SectionUpdatedDomainEvent : INotification
    {
        public SectionUpdatedDomainEvent(Section section)
        {
            this.Section = section;
        }

        public Section Section { get; }
    }
}
