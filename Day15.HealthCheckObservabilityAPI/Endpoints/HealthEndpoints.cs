//gs


using Day15.HealthCheckObservabilityAPI.Contracts;
using Day15.HealthCheckObservabilityAPI.Interfaces;
using System;

namespace Day15.HealthCheckObservabilityAPI.Endpoints
{
    public static class HealthEndpoints
    {
        public static void MapHealthEndpoints(this WebApplication app)
        {
            //basic health -point
            app.MapGet("/health",
                (ISystemInfoService systemInfoService) =>
                {
                    var status = systemInfoService.GetSystemStatus();

                    return Results.Ok(new ApiResponse<object>
                    {
                        Success = true,

                        Message = "System is Healthy...Yaay",

                        Data = status,

                        GeneratedAt = DateTime.UtcNow
                    });
                }
            );
            //
            //metadata --point
            app.MapGet("/metadata",
                (ISystemInfoService systemInfoService) =>
                {
                    var metadata = systemInfoService.GetSystemMetadata();

                    return Results.Ok(new ApiResponse<object>
                    {
                        Success = true,

                        Message = "MetaData retrieved Successfully.. yaay",

                        Data = metadata,

                        GeneratedAt = DateTime.UtcNow
                    });
                }
            );
            //
        }
    }
}