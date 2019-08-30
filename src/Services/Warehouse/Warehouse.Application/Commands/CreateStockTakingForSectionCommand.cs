using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateStockTakingForSectionCommand : IRequest<StockTaking>
    {
        public CreateStockTakingForSectionCommand(int sectionId, bool includeEmptyPositions)
        {
            this.SectionId = sectionId;
            this.IncludeEmptyPositions = includeEmptyPositions;
        }

        public int SectionId { get; }
        public bool IncludeEmptyPositions { get; }
    }
}
