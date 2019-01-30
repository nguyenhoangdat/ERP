using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class WarehouseFullException : Exception
    {
        public WarehouseFullException()
        {
        }

        public WarehouseFullException(string message) : base(message)
        {
        }

        public WarehouseFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WarehouseFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
