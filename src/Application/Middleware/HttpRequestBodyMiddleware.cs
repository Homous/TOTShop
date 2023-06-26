using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace Application.Middleware;

public class HttpRequestBodyMiddleware
{
    private readonly ILogger logger;
    private readonly RequestDelegate next;

    public HttpRequestBodyMiddleware(ILogger<HttpRequestBodyMiddleware> logger,
        RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            var query = context.Request.Query;
            var reader = new StreamReader(context.Request.Body);
            var builder = new StringBuilder(Environment.NewLine);
            var routes = new StringBuilder();
            foreach (var header in context.Request.Headers)
            {
                builder.AppendLine($"{header.Key}:{header.Value}");
            }

            foreach (var route in context.Request.Query)
            {
                routes.Append($"{route.Key}:{route.Value} ");
            }


            string body = await reader.ReadToEndAsync();
            logger.LogInformation(
                $"Request [Http{context.Request?.Method}] [Path:{context.Request?.Path}] [Parameters: {routes}] \nHeaders: {builder} \n Body: {body}\n");

            context.Request.Body.Position = 0L;

            await next(context);

            logger.LogInformation(
                $"Request [Http{context.Request?.Method}] [Path:{context.Request?.Path}] [StatusCode:{context.Response.StatusCode}]");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = new ResultModel() { Message = $"{ex.Message}", Data = "", Status = false };

            await context.Response.WriteAsJsonAsync(result);
        }

    }
}