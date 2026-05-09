//gs
using Day09.CachingSystem.Application.Services;

namespace Day09.CachingSystem.API
{
    //single responsibility principle
    //This file has one job
    //ie to define the HTTP endpoints for cache system

    //Endpoint should be thin
    //it should not contain cache logic
    //it should not know IMemoryCache
    //It only receives request,call application services and returns responses.

    public static class CacheEndpoints
    {
        public static void MapCacheEndpoints(this WebApplication app)
        {
            app.MapGet("/cache-demo/{key}", async (string key, CacheDemoService service) =>
            {
                var response = await service.GetCacheDemoAsync(key);

                return Results.Ok(response);
            });
        }
        
    }
}