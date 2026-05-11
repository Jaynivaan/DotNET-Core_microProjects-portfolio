//gs

using Day11.BackgroundJobs.Metadata;
using Day11.BackgroundJobs.Responses;
using Day11.BackgroundJobs.Services;
using System;


namespace Day11.BackgroundJobs.Endpoints
{
    //This class only maps http routes
    //endpoint handles transport layer only
    //this class is static as this class not storing state or creating objects.
    public static class JobStatusEndpoints
    {
        public static void MapJobStatusEndpoints(this WebApplication app)
        {
            //api end point to read worker heartbeat state.
            app.MapGet("/job-status", (IJobStatusService jobStatusService) =>
            {
                //get current worker background state
                var status = jobStatusService.GetStatus();

                //wrap inside consistent api response structure.
                var response = new ApiResponse<object>
                {
                    Success = true,

                    Message = "Background worker heartbeat retrieved successfully.",

                    Data = status,

                    Metadata = new ResponseMetadata
                    {
                        GeneratedAt = DateTime.UtcNow,

                        Environment = "Development",

                        Version = "v1"
                    }
                };
                return Results.Ok(response);
            });
        }
    }
}