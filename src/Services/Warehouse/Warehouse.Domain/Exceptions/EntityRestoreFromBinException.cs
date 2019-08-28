using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class EntityRestoreFromBinException : Exception
    {
        public EntityRestoreFromBinException()
        {
        }

        public EntityRestoreFromBinException(string message) : base(message)
        {
        }

        public EntityRestoreFromBinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityRestoreFromBinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
