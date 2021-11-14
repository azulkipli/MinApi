var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

// Redirect http to https
// app.UseHttpsRedirection();

// variable need to be define before
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", () => "Index .NET 6 Minimal API!");

app.MapGet("/hello", () => new { Hello = ".NET 6 Minimal API" })
   .WithName("HelloObject");

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

// run default port 5000 / 5001
// app.Run();

// load port from EnvironmentVariable
var appPort = Environment.GetEnvironmentVariable("APP_PORT") ?? "5000";

app.Run($"http://localhost:{appPort}");

// Function to generate list of temperatures
record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}