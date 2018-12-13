using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class EmptyPositionException : Exception
    {
        public EmptyPositionException()
        {
        }

        public EmptyPositionException(string message) : base(message)
        {
        }

        public EmptyPositionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyPositionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
