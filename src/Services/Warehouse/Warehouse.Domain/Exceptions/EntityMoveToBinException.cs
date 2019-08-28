using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class EntityMoveToBinException : Exception
    {
        public EntityMoveToBinException()
        {
        }

        public EntityMoveToBinException(string message) : base(message)
        {
        }

        public EntityMoveToBinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityMoveToBinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
