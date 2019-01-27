using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindSectionByIdCommand : IRequest<Section>
    {
        public FindSectionByIdCommand(FindSectionByIdCommandModel model)
        {
            this.Model = model;
        }
        public FindSectionByIdCommand(int sectionId)
            : this(new FindSectionByIdCommandModel(sectionId)) { }

        public FindSectionByIdCommandModel Model { get; }

        public class FindSectionByIdCommandModel
        {
            public FindSectionByIdCommandModel(int sectionId)
            {
                this.SectionId = sectionId;
            }

            public int SectionId { get; }
        }
    }
}
