//gs
using Day10.RateLimiter.Responses;
using Microsoft.AspNetCore.Builder;

namespace Day10.RateLimiter.Endpoints

//SRP principle is followed here
// this file only defines the http endpoints.
//this file maps urls to responses.
{
    public static class ShieldEndpoints

    {
        public static void MapShieldEndpoints(this WebApplication app)
        {
            //public endpoint
            //This end point is open and does not use ratelimit policy
            app.MapGet("/", () =>
            {
                return Results.Ok(new ApiResponse
                {
                    Success = true,
                    Message ="AI Storm Shield API is running",
                    Timestamp = DateTime.UtcNow

                });
            });

            //protected endpoint
            //This endpoint uses the named ratelimit policy from Program.cs
            app.MapGet("/shield", () =>
            {
                return Results.Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Request Accepted. You passed through the storm shield.",
                    Timestamp = DateTime.UtcNow
                });
            }).RequireRateLimiting("storm");
        }
    }
}
