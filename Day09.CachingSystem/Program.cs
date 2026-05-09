//gs

using Day09.CachingSystem.API;
using Day09.CachingSystem.Application.Interfaces;
using Day09.CachingSystem.Application.Services;
using Day09.CachingSystem.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ICacheService, MemoryCacheService>();
builder.Services.AddScoped<CacheDemoService>();

var app = builder.Build();

app.MapCacheEndpoints();

app.Run();