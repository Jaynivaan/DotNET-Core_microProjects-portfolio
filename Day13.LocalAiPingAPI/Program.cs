//gs
using Day13.LocalAiPingAPI.Endpoints;
using Day13.LocalAiPingAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

//register all infra structure services
builder.Services.AddLocalAiServices(builder.Configuration);

var app = builder.Build();

//map all endpoints
app.MapAiEndpoints();

//start the app

app.Run();
