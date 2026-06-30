//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Endpoints
{
    public static class GravityEndpoints
    {
        public static IEndpointRouteBuilder MapGravityEndpoints(this IEndpointRouteBuilder app)

        {
            var group = app.MapGroup("/gravity");

            group.MapGet(
                "/runtime",
                (IGravitySnapshotProvider provider) =>
                {
                    return Results.Ok(provider.GetSnapshot());
                })
                .WithName("GravityFieldRuntimeSnapshot")
                .WithSummary("Get the AEM-ESGF runtime snapshot.")
                .WithDescription("Returns readonly runtime diagnostics for emergent  semantic gravitational fields.")
                .WithTags("GravtiyField")
                .Produces<GravityRuntimeSnapshot>(StatusCodes.Status200OK);

            group.MapGet(
                "/statistics",
                (IGravityStatisticsProvider provider) =>
                {
                    return Results.Ok(provider.GetStatistics());
                })
                .WithName("GravityFieldRuntimeStatistics")
                .WithSummary("Get the AEM-ESGF runtime statistics.")
                .WithDescription("Returns cumulative runtime statistics for emergent  semantic gravitational fields.")
                .WithTags("GravtiyField")
                .Produces<GravityRuntimeStatistics>(StatusCodes.Status200OK);

            return app;

        }
    }
}
