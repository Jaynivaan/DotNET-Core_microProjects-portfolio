//gs
using System;

namespace Day23.RealityFeedEngine.Models
{
    public class FeedItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = "";

        public string Category { get; set; } = "";

        public int Importance { get; set; }

        public int Urgency { get; set; }

        public int Alignment { get; set; }

        public int EnergyRequired { get; set; }

    }

}