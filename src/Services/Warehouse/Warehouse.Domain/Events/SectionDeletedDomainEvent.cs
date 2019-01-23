using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class SectionDeletedDomainEvent : INotification
    {
        public SectionDeletedDomainEvent(Section section)
        {
            this.Section = section;
        }

        public Section Section { get; }
    }
}
