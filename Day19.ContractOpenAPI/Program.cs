//gs
using Day19.ContractOpenAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapOpenApiEndpoints();

app.Run();

