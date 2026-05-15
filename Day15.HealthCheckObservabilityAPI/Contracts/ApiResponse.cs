//gs

using System;

namespace Day15.HealthCheckObservabilityAPI.Contracts
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public T? Data { get; set; }

        public DateTime GeneratedAt { get; set; } 

    }
}

