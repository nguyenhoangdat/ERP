using MediatR;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class ValidateOrderCommand : IRequest<bool>
    {
        public ValidateOrderCommand(IEnumerable<ProductCount> wareCounts)
        {
            this.WareCounts = wareCounts;
        }

        public IEnumerable<ProductCount> WareCounts { get; }

        public class ProductCount
        {
            public ProductCount(int wareId, int count)
            {
                this.WareId = wareId;
                this.Count = count;
            }

            public int WareId { get; }
            public int Count { get; }
        }
    }
}
