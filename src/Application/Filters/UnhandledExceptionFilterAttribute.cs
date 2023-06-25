using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Filters;

public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<UnhandledExceptionFilterAttribute> _logger;

    public UnhandledExceptionFilterAttribute(ILogger<UnhandledExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {

        var result = new ObjectResult(new ResultModel()
        {
            Message = context.Exception.Message,
            Data = "",
            Status = false
        })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        // Log the exception
        _logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);

        // Set the result
        context.Result = result;
    }
}
