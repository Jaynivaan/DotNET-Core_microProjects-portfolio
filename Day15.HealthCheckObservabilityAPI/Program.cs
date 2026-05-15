//gs
using Day15.HealthCheckObservabilityAPI.Config;
using Day15.HealthCheckObservabilityAPI.Endpoints;
using Day15.HealthCheckObservabilityAPI.Interfaces;
using Day15.HealthCheckObservabilityAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//BUilt in .net health sytem
builder.Services.AddHealthChecks();

//bind options
builder.Services.Configure<AppInfoOptions>(
    builder.Configuration.GetSection("AppInfo")
    );

//register observability
//healthcheck and observability is different

builder.Services.AddScoped<ISystemInfoService, SystemInfoService>();

var app = builder.Build();

//builtin raw health check endpoint
app.MapHealthChecks("/healthz");

//our custom crafted very thin endpoints
app.MapHealthEndpoints();

//start the app
app.Run();

