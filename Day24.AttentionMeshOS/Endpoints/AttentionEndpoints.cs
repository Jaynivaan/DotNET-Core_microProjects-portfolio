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
            app.MapPost("/attention/input",
                async (AttentionRequest request,
                IAttentionEngine attentionEngine,
                CancellationToken cancellationToken )=>
                {
                    var result = await attentionEngine.ProcessAsync(
                        request.UserInput,
                        cancellationToken);

                    if (!result .IsSuccess )
                    {
                        return Results.BadRequest(
                            result.InvalidInputResponse);
                    }
                    return Results.Ok(result.Response);

                }
            )
                .WithName("AttentionInput")
                .WithSummary("Accepts raw attention input and generates a persistence shot.")
                .WithDescription("Stores the raw input , creates an attentionBall builds mesh and returns a persistence shot")
                .WithTags("Input")
                .Produces<AttentionResponse>(StatusCodes.Status200OK);

            app.MapGet("/attention/raw-inputs",
                (IRawAttentionInputStore store) =>
                {
                    return Results.Ok(store.GetAll());

                })
                .WithName("Get Attention Inputs")
                .WithSummary("Returns stores Raw attentionInputs")
                .WithDescription("Returns All raw inputs received by the attention system.")
                .WithTags("Input");


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