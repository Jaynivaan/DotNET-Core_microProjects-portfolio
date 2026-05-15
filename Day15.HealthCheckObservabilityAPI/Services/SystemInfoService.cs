//gs

using Day15.HealthCheckObservabilityAPI.Config;
using Day15.HealthCheckObservabilityAPI.Interfaces;
using Day15.HealthCheckObservabilityAPI.Models;
using Microsoft.Extensions.Options;
using System;


namespace Day15.HealthCheckObservabilityAPI.Services
{

    // the observability logic live here
    public class SystemInfoService : ISystemInfoService
    {
        //store app start up time
        private static readonly DateTime _startedAt = DateTime.UtcNow;

        private readonly AppInfoOptions _appInfo;

        //option pattern injection
        public SystemInfoService (IOptions<AppInfoOptions> appInfoOptions)
        {
            _appInfo = appInfoOptions.Value;
        }

        //current system state
        public SystemStatus GetSystemStatus()
        {
            return new SystemStatus
            {
                IsHealthy = true,

                IsRunning = true,

                StartedAt = _startedAt,

                //uptime computations
                UptimeMinutes = (DateTime.UtcNow - _startedAt).TotalMinutes,

                Environment = _appInfo.EnvironmentName
            };
        }
        //system Metadata
        public SystemMetadata GetSystemMetadata()
        {
            return new SystemMetadata
            {
                AppName = _appInfo.AppName,

                Version = _appInfo.Version,

                Environment = _appInfo.EnvironmentName,

                MachineName = Environment.MachineName,

                ProcessorCount = Environment.ProcessorCount,

                GeneratedAt = DateTime.UtcNow
            };

        }


    }
}