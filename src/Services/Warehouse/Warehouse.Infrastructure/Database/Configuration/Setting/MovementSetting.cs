namespace Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration.Setting
{
    public class MovementSetting : IMovementSetting
    {
        public MovementSetting(int? monthsRetentionPeriod)
        {
            this.MonthsRetentionPeriod = monthsRetentionPeriod;
        }

        public int? MonthsRetentionPeriod { get; }
    }
}
