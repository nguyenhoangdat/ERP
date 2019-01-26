using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindStockTakingByIdCommand : IRequest<StockTaking>
    {
        public FindStockTakingByIdCommand(FindStockTakingByIdCommandModel model)
        {
            this.Model = model;
        }
        public FindStockTakingByIdCommand(int id) : this(new FindStockTakingByIdCommandModel(id))
        {
        }

        public FindStockTakingByIdCommandModel Model { get; }

        public class FindStockTakingByIdCommandModel
        {
            public FindStockTakingByIdCommandModel(int id)
            {
                this.Id = id;
            }

            public int Id { get; }
        }
    }
}
