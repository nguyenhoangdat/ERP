using Restmium.ERP.Services.Warehouse.Infrastructure.Database;

namespace Restmium.ERP.Services.Warehouse.Tests.Common.Interfaces
{
    public interface IDbSeeder
    {
        void Seed(DatabaseContext databaseContext);
    }
}
