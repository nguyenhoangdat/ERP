using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class PositionEmptyException : Exception
    {
        public PositionEmptyException()
        {
        }

        public PositionEmptyException(string message) : base(message)
        {
        }

        public PositionEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
