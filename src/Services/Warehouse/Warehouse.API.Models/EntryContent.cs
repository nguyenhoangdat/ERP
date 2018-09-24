namespace Warehouse.API.Models
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
