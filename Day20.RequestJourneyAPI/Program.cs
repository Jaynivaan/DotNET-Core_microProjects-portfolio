//gs
using Day20.RequestJourneyAPI.Middleware;
using Day20.RequestJourneyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RequestJourneyService>();

var app = builder.Build();

//app.UseStaticFiles(); //another middle ware for front end. serve files from wwwroot

app.UseMiddleware<RequestJourneyMiddleware>();

app.MapGet("/journey", (RequestJourneyService service) =>
{
    return Results.Ok(new
    {
        message = service.GetJourneyMessage(),
        path = "/journey",
        stage = "Endpoint executed",
        time = DateTime.Now
    });
});

app.MapGet("/", () =>
{
    return Results.Text("Day20.RequestJourneyAPI is running. Open  /journey to text middle ware flow."); 
});

app.Run();

