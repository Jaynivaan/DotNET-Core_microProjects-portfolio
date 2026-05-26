//gs
using Day23.RealityFeedEngine.Models;
using Day23.RealityFeedEngine.Shared;
using Day23.RealityFeedEngine.Services.Interfaces;


namespace Day23.RealityFeedEngine.Services
{
    public class FeedService : IFeedService
    {
        public ApiResponse<FeedResponse> GetFeed()
        {
            var candidates = new List<FeedItem>
            {
                new FeedItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Review Dependency Injection",
                    Category = "CSharp",
                    Importance = 9,
                    Urgency = 7,
                    Alignment = 10,
                    EnergyRequired = 4
                },
                new FeedItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Build OpenAPI contract project",
                    Category = "Backend standards",
                    Importance = 8,
                    Urgency = 6,
                    Alignment = 9,
                    EnergyRequired = 5

                },
                new FeedItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Watch random tech videos",
                    Category = "distraction",
                    Importance = 2,
                    Urgency =  1,
                    Alignment = 2,
                    EnergyRequired = 3

                },

                new FeedItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Refactor yesterday Project",
                    Category = "Engineering",
                    Importance = 7,
                    Urgency = 5,
                    Alignment = 8,
                    EnergyRequired = 6
                },

                new FeedItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Practice one Csharp interview question",
                    Category = "Career",
                    Importance = 10,
                    Urgency = 8,
                    Alignment = 10,
                    EnergyRequired = 3

                }

            };

            var rankedItems = candidates
                .Select(item =>
                {
                    var score =
                        item.Importance +
                        item.Urgency +
                        item.Alignment -
                        item.EnergyRequired;

                    return new RankedFeedItem
                    {
                        Title = item.Title,
                        Category = item.Category,
                        Score = score,
                        Reason =
                            $"Score = importance {item.Importance} + urgency {item.Urgency} + alignment {item.Alignment} - energy {item.EnergyRequired}"
                    };

                })
                .OrderByDescending(item => item.Score)
                .Take(3)
                .ToList();

            return new ApiResponse<FeedResponse>
            {
                Success = true,

                Message = " Reality Feed generated Successfully.",

                Data = new FeedResponse

                {
                    Items = rankedItems,

                    TotalCandidates = candidates.Count,

                    GeneratedAt = DateTime.UtcNow
                }


            };
        }
    }
}