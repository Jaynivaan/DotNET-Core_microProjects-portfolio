//gs
using Day23.RealityFeedEngine.Extensions;
using Day23.RealityFeedEngine.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddRealityFeedServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapFeedEndpoints();

app.Run();
