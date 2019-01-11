using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class PositionLoadCapacityException : Exception
    {
        public PositionLoadCapacityException()
        {
        }

        public PositionLoadCapacityException(string message) : base(message)
        {
        }

        public PositionLoadCapacityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionLoadCapacityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
