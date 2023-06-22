using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        context.Request.EnableBuffering();

        var reader = new StreamReader(context.Request.Body);

        string body = await reader.ReadToEndAsync();
        logger.LogInformation(
            $"Request [Http{context.Request?.Method}] [Path{context.Request?.Path}]\n Body: {body}");

        context.Request.Body.Position = 0L;

        await next(context);

        logger.LogInformation(
            $"Request [Http{context.Request?.Method}] [Path:{context.Request?.Path}] [StatusCode:{context.Response.StatusCode}]");
    }
}