//gs
using Day21.FeatureflagAPI.Responses;
using Day21.FeatureflagAPI.Services.Interfaces;

namespace Day21.FeatureflagAPI.Endpoints
{
    public static class FeatureEndpoints
    {
        public static void MapFeatureEndpoints(this WebApplication app)
        {
            app.MapGet("/features",
                (IFeatureService service) =>
                {
                    var result = new ApiResponse<object>
                    {
                        Success = true,
                        Message = "Feature list retrieved",
                        Data = service.GetFeature()
                    };

                    return Results.Ok(result); 

                });

            app.MapGet("/features/darkmode",
                (IFeatureService service) =>
                {
                    var feature = service.GetFeature();

                    return Results.Ok(
                        new
                        {
                            feature.DarkMode
                        });
                });

            app.MapGet("/features/aichat",
                (IFeatureService service) =>
                {
                    var feature = service.GetFeature();

                    return Results.Ok(
                        new
                        {
                            feature.AiChat
                        });
                });

            app.MapGet("/features/betadashboard",
                (IFeatureService service) =>
                {
                    var feature = service.GetFeature();

                    return Results.Ok(
                        new
                        {
                            feature.BetaDashboard
                        });

                });
        }
    }
}