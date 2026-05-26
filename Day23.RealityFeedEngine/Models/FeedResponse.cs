//gs
using System;
using System.Collections.Generic;

namespace Day23.RealityFeedEngine.Models
{
    public class FeedResponse
    {
        public List<RankedFeedItem> Items { get; set; } = new();

        public int TotalCandidates { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}