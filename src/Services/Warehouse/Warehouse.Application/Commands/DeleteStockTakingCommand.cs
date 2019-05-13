using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteStockTakingCommand : IRequest<StockTaking>
    {
        public DeleteStockTakingCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; }
    }
}
