namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class Movement
    {
        public enum EntryContent
        {
            Receipt, //příjem
            Delivery, //výdej
            PositionTransfer //převod mezi pozicemi
        }
    }
}
