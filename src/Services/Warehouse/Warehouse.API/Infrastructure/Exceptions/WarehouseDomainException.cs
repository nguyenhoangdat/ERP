using System;
using System.Runtime.Serialization;

namespace Warehouse.API.Infrastructure.Exceptions
{
    public class WarehouseDomainException : Exception
    {
        public WarehouseDomainException()
        {
        }

        public WarehouseDomainException(string message) : base(message)
        {
        }

        public WarehouseDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WarehouseDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
