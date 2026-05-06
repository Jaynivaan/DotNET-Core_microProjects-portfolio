//gs
using Day06_WeatherApi.Services;
using Day06_WeatherApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Register http client factory
builder.Services.AddHttpClient();

//Register Weather service
builder.Services.AddScoped<IWeatherService, WeatherService>();

//bind weather api section from appsettings.json
builder.Services.Configure<WeatherApiOption>(
    builder.Configuration.GetSection("WeatherApi"));

var app = builder.Build();

app.MapGet("/weather/{city}", async
(
    string city,
    IWeatherService weatherService
) =>
{
    var result = await weatherService.GetWeatherAsync(city);

    if (result == null)
    {
        return Results.BadRequest(new
        {
            Success = false,
            Message = "Could not fetch weather data."

        });
    }
    return Results.Ok(new
    {
        Success = true,
        Data = result
    });
});

//start app

app.Run();
