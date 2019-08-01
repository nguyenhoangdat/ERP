using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Validators
{
    public class DefaultOrderValidator : IOrderValidator
    {
        public DefaultOrderValidator(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        public DatabaseContext DatabaseContext { get; }

        public bool IsValid(int wareId, int count)
        {
            Ware ware = this.DatabaseContext.Wares.Find(new object[] { wareId });

            return ware == null ? false : this.DatabaseContext.Positions.Where(x => x.GetWare().Id == wareId).Sum(x => x.CountWare()) >= count;
        }
    }
}
