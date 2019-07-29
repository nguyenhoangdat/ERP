using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class PositionSpaceCapacityException : Exception
    {
        public PositionSpaceCapacityException()
        {
        }

        public PositionSpaceCapacityException(string message) : base(message)
        {
        }

        public PositionSpaceCapacityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionSpaceCapacityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
