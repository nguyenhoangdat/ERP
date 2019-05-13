using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWaresInSectionCommand : IRequest<IEnumerable<Ware>>
    {
        public FindWaresInSectionCommand(int sectionId)
        {
            this.SectionId = sectionId;
        }

        public int SectionId { get; }
    }
}
