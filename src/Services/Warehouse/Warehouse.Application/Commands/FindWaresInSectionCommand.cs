using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWaresInSectionCommand : IRequest<IEnumerable<Ware>>
    {
        public FindWaresInSectionCommand(FindWaresInSectionCommandModel model)
        {
            this.Model = model;
        }
        public FindWaresInSectionCommand(int sectionId)
            : this(new FindWaresInSectionCommandModel(sectionId)) { }

        public FindWaresInSectionCommandModel Model { get; }

        public class FindWaresInSectionCommandModel
        {
            public FindWaresInSectionCommandModel(int sectionId)
            {
                this.SectionId = sectionId;
            }

            public int SectionId { get; }
        }
    }
}
