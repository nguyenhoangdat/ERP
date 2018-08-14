using System;

namespace Ordering.API.Models
{
    [Flags]
    public enum OrderStatus
    {
        Ordered = 1, //Objednáno
        Pending = 2, //Čekající na vyřízení
        Processing = 4, //Vyřizováno
        Processed = 8, //Vyřízeno
        Shipped = 16, //Odesláno
        Complete = 32, //Převzato zákazníkem

        Cancelled = 64,
        Failed = 128, //Failed to deliver

        Refunded = 256, //Vráceny peníze - 
        Expired = 512 //Expirováno (nevyzvednuto) - The order has expired
    }
}
