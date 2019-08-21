using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class UnitsExceededException : Exception
    {
        public UnitsExceededException()
        {
        }

        public UnitsExceededException(string message) : base(message)
        {
        }

        public UnitsExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnitsExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
