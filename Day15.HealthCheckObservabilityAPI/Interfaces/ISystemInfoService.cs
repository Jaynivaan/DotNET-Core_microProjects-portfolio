//gs
using Day15.HealthCheckObservabilityAPI.Models;

namespace Day15.HealthCheckObservabilityAPI.Interfaces
{
    public interface ISystemInfoService
    {
        SystemStatus GetSystemStatus();

        SystemMetadata GetSystemMetadata();

    }
}