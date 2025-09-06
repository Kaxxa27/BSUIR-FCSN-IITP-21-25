using Serilog.Core;

namespace WEB_153503_Kakhnouski.Middleware;

public class LogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Logger _logger;
    public LogMiddleware(RequestDelegate next, Logger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next.Invoke(context);

        int code = context.Response.StatusCode;

        if (code >= 300 || code < 200)
        {
            _logger.Information($"---> request {context.Request.Path + context.Request.QueryString.ToUriComponent()} {code}");
        }

    }
}
