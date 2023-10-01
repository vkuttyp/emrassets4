using Assets.Api;
using Assets.Data.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
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
    opt.WithOrigins("https://localhost:5173");
    opt.AllowAnyOrigin();
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
});
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (ClaimsPrincipal user) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast").RequireAuthorization();
//.WithOpenApi();

// var group=app.MapGroup("/users");
app.MapPost("users/login", (LoginData loginData, ILogger<Program> logger) =>
{
    string key = config["JwtSettings:Key"]!;
    string issuer = config["JwtSettings:Issuer"]!;
    string audience = config["JwtSettings:Audience"]!;
    var u = new UserTest("vkuttyp@gmail.com", "00", "Miran", "Prog", 1, 2, "some");
    logger.LogInformation($"{u}");
    var token = Helpers.BuildToken(u, key, issuer, audience);
    u.Token = token;
    return Results.Ok(u);
});
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record LoginData(string email, string password);

public class UserTest
{
    public UserTest(string email, string id, string name, string title, int groupId, int userTypeId, string loginId)
    {
        Email = email;
        this.id = id;
        this.name = name;
        Title = title;
        GroupId = groupId;
        UserTypeId = userTypeId;
        LoginId = loginId;
    }

    public string Email { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string Title { get; set; }
    public int GroupId { get; set; }
    public int UserTypeId { get; set; }
    public string LoginId { get; set; }
    public string Token { get; set; }
}

