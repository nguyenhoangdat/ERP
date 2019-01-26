using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateStockTakingForSectionCommand : IRequest<StockTaking>
    {
        public CreateStockTakingForSectionCommand(CreateStockTakingForSectionCommandModel model)
        {
            this.Model = model;
        }
        public CreateStockTakingForSectionCommand(int sectionId)
            : this(new CreateStockTakingForSectionCommandModel(sectionId)) { }

        public CreateStockTakingForSectionCommandModel Model { get; }

        public class CreateStockTakingForSectionCommandModel
        {
            public CreateStockTakingForSectionCommandModel(int sectionId)
            {
                this.SectionId = sectionId;
            }

            public int SectionId { get; }
        }
    }
}
