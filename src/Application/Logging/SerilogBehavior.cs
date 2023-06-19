using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Logging;

public class SerilogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{

    private readonly ILogger<SerilogBehavior<TRequest, TResponse>> _logger;

    public SerilogBehavior(ILogger<SerilogBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName},{DateTimeUtc}",
            typeof(TRequest).Name, DateTime.UtcNow);

        var result = await next();

        _logger.LogInformation("Completed request {@RequestName},{@DateTimeUtc}",
            typeof(TRequest).Name, DateTime.UtcNow);

        return result;
    }
}
