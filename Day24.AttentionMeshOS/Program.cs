//gs
using Day24.AttentionMeshOS.Endpoints;
using Day24.AttentionMeshOS.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAttentionMesh(builder.Configuration );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapAttentionEndpoints();
app.MapHealthChecks("/health");
app.Run();


