using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class SectionCreatedDomainEvent : INotification
    {
        public SectionCreatedDomainEvent(Section section)
        {
            this.Section = section;
        }

        public Section Section { get; }
    }
}
