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
                "/balls/{id:guid}",
                (Guid id, IAttentionReleaseService releaseService ) =>
                {
                    var response = releaseService.Release(id);

                    return response.Succeeded
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

            group.MapDelete("/balls",
                (IAttentionReleaseService releaseService) =>
                {
                    var response = releaseService.ReleaseAll();

                    return Results.Ok(response);

                })
                .WithName("ReleaseAllAttentionBalls")
                .WithSummary("Release all attentionBalls all at once.")
                .WithDescription("Removes all the attentionballs, link and reinforcementEvents leaving only the raw input behind . and persist the changes in store.")
                .WithTags("Release")
                .Produces<DeleteResponse>(StatusCodes.Status200OK);

            group.MapDelete("/raw-inputs/{id:guid}",
                (Guid id, IRawInputReleaseService releaseService) =>
                {
                    var response = releaseService.Release(id);

                    return response.Succeeded
                        ? Results.Ok(response)
                        : Results.NotFound(response);
                })
                .WithName("ReleaseRawInput")
                .WithSummary("Release a RawAttention Input.")
                .WithDescription("Deletes one raw input while preserving associated AttentionBalls.")
                .WithTags("Release")
                .Produces<DeleteResponse>(StatusCodes.Status200OK)
                .Produces<DeleteResponse>(StatusCodes.Status404NotFound);

            group.MapDelete("/raw-inputs",
                (bool confirm, IRawInputReleaseService releaseService) =>
                {
                    var response = releaseService.ReleaseAll(confirm);

                    return response.Succeeded
                        ? Results.Ok(response)
                        : Results.NotFound(response);
                })
                .WithName("ReleaseAllRawInputs")
                .WithSummary("Release all raw inputs at once")
                .WithDescription("Deletes all raw inputs only when confirm = true. Associated AttentionBalls are Preserved.")
                .WithTags("Release")
                .Produces<DeleteResponse>(StatusCodes.Status200OK)
                .Produces<DeleteResponse>(StatusCodes.Status400BadRequest);


                
            return app;

        }
    }
}