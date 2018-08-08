namespace Warehouse.API.Models
{
    public enum EntryContent
    {
        Balance, //zůstatek
        Receipt, //příjem
        Delivery, //výdej
        PositionTransfer //převod mezi pozicemi
    }
}
