//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Day24.AttentionMeshOS.Endpoints
{
    public static class AttentionEndpoints
    {
        public static IEndpointRouteBuilder MapAttentionEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/attention/shot",
                (AttentionRequest request,
                IAttentionEngine attentionEngine )=>
                {
                    var response = attentionEngine.Process(request.UserInput);

                    return Results.Ok(response);

                }
            )
                .WithName("CreateAttentionShot")
                .WithSummary("This endpoint creates a new attention shot based on the user input.")
                .WithDescription("This endpoint allows users to create a new attention shot by providing their input.")
                .WithTags("Attention")
                .Produces<AttentionResponse>(StatusCodes.Status200OK);

            app.MapGet("/attention/State",
                (IAttentionStateService stateService) =>
                {
                    var response = stateService.GetState();

                    return Results.Ok(response);


                }
            )
                .WithName("GetAttentionState")
                .WithSummary("This endpoint retrieves the current state of the attention mesh, including active attention balls and their relationships.")
                .WithDescription("Returns all AttentionBalls currently stored in the attentionMeshOS state")
                .WithTags("Observability")
                .Produces<AttentionStateResponse>(StatusCodes.Status200OK);

            app.MapGet("/attention/anchors",
                (IAnchorStateService anchorStateService) =>
                {
                    var response = anchorStateService.GetAnchors();

                    return Results.Ok(response);
                })
                .WithName("GetAnchorAttention")
                .WithSummary("Retrieves anchor attention ")
                .WithDescription("This endpoint returns attentionballs marked as anchors.")
                .WithTags("Anchors")
                .Produces<IReadOnlyList<AnchorAttentionResponse>>(StatusCodes.Status200OK);

            return app;

        }
    }
}