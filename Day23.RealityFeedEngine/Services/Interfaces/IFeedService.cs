//gs
using Day23.RealityFeedEngine.Models;
using Day23.RealityFeedEngine.Shared;

namespace Day23.RealityFeedEngine.Services.Interfaces
{
    public interface IFeedService
    {
        ApiResponse<FeedResponse> GetFeed();
    }
}