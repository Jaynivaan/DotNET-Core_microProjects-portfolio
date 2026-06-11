//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Endpoints
{
    public static class ReleaseEndpoints
    {
        public static IEndpointRouteBuilder MapReleaseEndpoints (this IEndpointRouteBuilder app)

        {
            var group = app.MapGroup("/attention");

            group.MapGet(
                "/release-candidates",
                (IAttentionReleaseCandidateService service) =>
                {
                    var candidates = service
                        .GetReleaseCandidates()
                        .Where(candidates =>
                        candidates.IsReleaseCandidate)
                        .ToList();

                    return Results.Ok(candidates);

                })
                .WithName("GetReleaseCandidates")
                .WithSummary("Retrieves attentionBalls that are candidates for release.")
                .WithDescription("Retrieves attentionBalls that appear ready for release based the configured release awareness policy setting.. this endpoint doesnt release or delete anything.")
                .WithTags("Release")
                .Produces<IReadOnlyList<AttentionReleaseCandidateResponse>>(StatusCodes.Status200OK);

            group.MapDelete(
                "/{id:guid}",
                (Guid id, IAttentionReleaseService releaseService ) =>
                {
                    var released = releaseService.Release(id);

                    return released
                        ? Results.Ok($"AttentionBall {id} released.")
                        : Results.NotFound($"AttentionBall {id} not found. ");
                }
                )
                .WithName("ReleaseAttentionBall")
                .WithSummary("Release an AttentionBall from  the active Mesh")
                .WithDescription(
                    "Removes an attentionBall from attentionMesh and persists the change.")
                .WithTags("Release")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
            return app;

        }
    }
}