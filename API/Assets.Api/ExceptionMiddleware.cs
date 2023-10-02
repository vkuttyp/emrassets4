using Assets.Data.Models;
using System.Text.Json;

public class ExceptionMiddleware : IMiddleware
{
    readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            string detail = ex.StackTrace ?? "";
            var ss = detail.Split('\r');
            string exx = "";
            var l = ss?.Length ?? 0;
            if (l == 0) exx = ex.Message + detail;
            else
            {
                foreach (string s2 in ss)
                {
                    if (!s2.StartsWith("\n   at Microsoft")) exx += s2;
                }
            }
            var problem = new Problem(500, "Server Error", ex.Message, exx);
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);

        }
    }
    
}

