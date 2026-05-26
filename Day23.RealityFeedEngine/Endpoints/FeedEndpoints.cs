//gs
using Day23.RealityFeedEngine.Services.Interfaces;

namespace Day23.RealityFeedEngine.Endpoints
{
    public static class FeedEndpoints
    {
        public static void MapFeedEndpoints(this WebApplication app)
        {
            app.MapGet("/api/feed", (IFeedService service) =>
            {
                var result = service.GetFeed();

                return Results.Ok(result);
            })
             .WithName("GetRealityFeed")
             .WithSummary("Generate Ranked reality feed");
        }
    }
}