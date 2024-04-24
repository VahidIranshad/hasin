using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Hasin.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>

     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull, IRequest<TResponse>
     where TResponse : notnull
    {
        public ILogger<LoggingBehavior<TRequest, TResponse>> _logger { get; }
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
                _logger.LogWarning($"[PERFORMANCE] The request {typeof(TRequest).Name} took {timeTaken.Seconds} seconds.");

            return response;
        }
    }
}
