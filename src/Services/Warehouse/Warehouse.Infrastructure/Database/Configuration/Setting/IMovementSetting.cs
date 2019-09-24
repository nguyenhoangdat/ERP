namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration.Setting
{
    public interface IMovementSetting
    {
        int? MonthsRetentionPeriod { get; }
    }
}