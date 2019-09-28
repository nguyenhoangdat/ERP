using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWaresInSectionCommand : IRequest<IEnumerable<Ware>>
    {
        public FindWaresInSectionCommand(int sectionId)
        {
            if (sectionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(sectionId));
            }

            this.SectionId = sectionId;
        }

        public int SectionId { get; }
    }
}
