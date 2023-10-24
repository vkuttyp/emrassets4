using Assets.Api;
using Assets.Data.Models;
using Serilog;
using Serilog.Events;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    //.Filter.ByExcluding(evt =>
    //{

    //    evt.Properties.TryGetValue("RequestPath", out var endpoint);
    //    var path = LogEnricher.GetPropertyValue(endpoint);
    //    evt.Properties.TryGetValue("ClientIP", out var ip);
    //    bool isNotification = path.StartsWith("/api/cases/GetCaseByIdForNotification");
    //    return !path.StartsWith("/api") || isNotification || string.IsNullOrWhiteSpace(ip?.ToString());
    //})
    .WriteTo.Console(
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Indent:l}{Message}  [IP:{ClientIP} User: {UserId}] {NewLine}{Exception}")
    .WriteTo.File(
    @"/var/www/logs/log-.txt", Serilog.Events.LogEventLevel.Verbose,
    "{Timestamp:yyyy-MM-dd HH:mm:ss} {Indent:l}{Message}  [IP:{ClientIP} User: {UserId}] {NewLine}{Exception}", null, null, null, false, false, null, RollingInterval.Day
    )
    .CreateLogger();

builder.Services.AddMyServices(builder.Configuration);
var config = builder.Configuration;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseCors(opt =>
{
    opt.WithOrigins("http://localhost", "https://localhost:5173");
    opt.AllowAnyOrigin();
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
});
app.UseMiddleware<ExceptionMiddleware>();
app.UseResponseCompression();
app.UseSerilogRequestLogging(opts =>
{
    opts.EnrichDiagnosticContext = LogEnricher.EnrichFromRequest;
});
app.UseAuthorization();

app.MapControllers();

//.WithOpenApi();

// var group=app.MapGroup("/users");
//app.MapPost("users/login", (LoginData loginData, ILogger<Program> logger) =>
//{
//    string key = config["JwtSettings:Key"]!;
//    string issuer = config["JwtSettings:Issuer"]!;
//    string audience = config["JwtSettings:Audience"]!;
//    var u = new UserTest("vkuttyp@gmail.com", "00", "Miran", "Prog", 1, 2, "some");
//    logger.LogInformation($"{u}");
//    var token = Helpers.BuildToken(u, key, issuer, audience);
//    u.Token = token;
//    return Results.Ok(u);
//});
app.Run();



public static class LogEnricher
{
    public static string GetClientIpAddress(HttpContext context)
    {
        // Try to get the client IP address from the X-Real-IP header
        var clientIp = context.Request.Headers["X-Real-IP"].FirstOrDefault() ?? string.Empty;

        // If the X-Real-IP header is not present, fall back to the RemoteIpAddress property
        if (string.IsNullOrEmpty(clientIp))
        {
            clientIp = context.Connection.RemoteIpAddress.ToString();
        }

        return clientIp;
    }
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        //httpContext.Request.Headers.TryGetValue("clientip", out var header);
        //var ip = header.FirstOrDefault();
        //var ip = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
        var ip = GetClientIpAddress(httpContext);
        diagnosticContext.Set("ClientIP", ip);// httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown");
        //diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
        diagnosticContext.Set("UserId", httpContext?.User?.FindFirstValue("UserId") ?? "Anonymous");
        diagnosticContext.Set("RequestPath", httpContext?.Request?.Path ?? "Unknown");
    }
    public static string GetPropertyValue(LogEventPropertyValue? propertyValue)
    {
        if (propertyValue == null) return "";
        if (propertyValue is ScalarValue)
        {
            var aa = propertyValue as ScalarValue;
            return aa?.Value?.ToString() ?? "";
        }
        return "";
    }
}