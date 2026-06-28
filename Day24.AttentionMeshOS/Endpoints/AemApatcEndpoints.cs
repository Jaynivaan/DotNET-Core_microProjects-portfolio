//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.AspNetCore.Routing;

namespace Day24.AttentionMeshOS.Endpoints
{
    public static class AemApatcEndpoints
    {
        public static IEndpointRouteBuilder MapAemApatcEndpoints(
            this IEndpointRouteBuilder app )
        {
            var group = app.MapGroup("/aem-apatc");

            group.MapGet(
                "/runtime",
                (IRuntimeSnapshotProvider snapshotProvider) =>
                {
                    var snapshot = snapshotProvider.GetSnapshot();

                    return Results.Ok(snapshot);
                })
                .WithName("GetAemApatcRuntime")
                .WithSummary("Returns the current AEM-APATC Runtime Snapshot.")
                .WithDescription("Provides a readonly diagnostics view of the Crystallization runtime.")
                .WithTags("AEM-APATC")
                .Produces<RuntimeSnapshot>(StatusCodes.Status200OK);

            group.MapGet(
                "/health",
                (IRuntimeHealthProvider healthProvider) =>
                {
                    var health = healthProvider.GetHealth();

                    return Results.Ok(health);
                })
                .WithName("GetAemApatcHealth")
                .WithSummary("Returns the operational Health of the AEM-APATC runtime.")
                .WithDescription("Provides a readonly operational readiness view of the semantic runtime.")
                .WithTags("AEM-APATC")
                .Produces<RuntimeHealth>(StatusCodes.Status200OK);

            group.MapGet(
                "/statistics",
                (IRuntimeStatisticsProvider statisticsProvider) =>
                {
                    var statistics = statisticsProvider.GetStatistics();

                    return Results.Ok(statistics);
                })
                .WithName("GetAEM-ApatcStatistics")
                .WithSummary("Returns cumulative runtime statistics for the AEM-APATC runtime.")
                .WithDescription("Provides lifetime operational statistics without modifying semantic runtimes state.")
                .WithTags("AEM-APATC")
                .Produces<RuntimeStatistics>(StatusCodes.Status200OK);

            group.MapGet(
                "/benchmark",
                (IPerformanceBenchmarkProvider benchmarkProvider) =>
                {
                    var benchmark = benchmarkProvider.GetBenchmark();

                    return Results.Ok(benchmark);
                })
                .WithName("GetAemApatcBenchmark")
                .WithSummary("Returns the AEM-APATC performance benchmark baseline.")
                .WithDescription("Provides baseline performance metrics for the semantic runtime.")
                .WithTags("AEM-APATC")
                .Produces<PerformanceBenchmark>(StatusCodes.Status200OK);

            return app;
        }
    }
}