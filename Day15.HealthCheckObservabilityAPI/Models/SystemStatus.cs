//gs

using System;

namespace Day15.HealthCheckObservabilityAPI.Models
{
    public class SystemStatus
    {
        public bool IsHealthy { get; set; }

        public bool IsRunning { get; set; }

        public DateTime StartedAt { get; set; }

        public double UptimeMinutes { get; set; }

        public string Environment { get; set; } = "";
    }
}

