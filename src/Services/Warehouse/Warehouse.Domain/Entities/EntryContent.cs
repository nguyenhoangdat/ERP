namespace Restmium.ERP.Services.Warehouse.Domain.Entities
{
    public partial class Movement
    {
        public enum EntryContent
        {
            Balance, //zůstatek
            Receipt, //příjem
            Delivery, //výdej
            PositionTransfer //převod mezi pozicemi
        }
    }
}
