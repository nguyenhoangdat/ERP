using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Infrastructure
{
    /// <summary>
    /// A RequestLogger instance represents a PreProcessor pipeline behaviour in MediatR. It logs every request made within the MediatR.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        protected const string RequestLoggerLogInformation = "Warehouse.API Request: {0} {1}";

        public RequestLogger(ILogger<TRequest> logger) 
        {
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation(RequestLoggerLogInformation, typeof(TRequest).Name, request);

            return Task.CompletedTask;
        }
    }
}
