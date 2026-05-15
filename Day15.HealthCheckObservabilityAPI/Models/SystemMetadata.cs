//gs

using System;

namespace Day15.HealthCheckObservabilityAPI.Models
{
    //extra observability information about the system..
    public class SystemMetadata
    {
        public string AppName { get; set; } = "";

        public string Version { get; set; } = "";

        public string Environment { get; set; } = "";

        public string MachineName { get; set; } = "";

        public int ProcessorCount { get; set; } 

        public DateTime GeneratedAt { get; set; }

    }
}

