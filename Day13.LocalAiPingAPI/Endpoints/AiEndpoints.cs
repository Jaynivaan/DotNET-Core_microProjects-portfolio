//gs
using Day13.LocalAiPingAPI.Interfaces;
using Day13.LocalAiPingAPI.Models;
using Day13.LocalAiPingAPI.Responses;
using System;
using System.IO;

namespace Day13.LocalAiPingAPI.Endpoints
{
    public static class AiEndPoints
    {
        public static void MapAiEndpoints(this WebApplication app)
        {
            app.MapPost("/ai/ping",
                async
                (
                    AiRequest request,
                    IAiService aiService
                ) =>
                {
                    //Defensive validation
                    if (string .IsNullOrWhiteSpace(request.Prompt))
                    {
                        return Results.BadRequest(new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Prompt is Required"
                        });
                    }

                    //call ai service layer
                    var aiResponse = await aiService.GenerateAsync(request);

                    //Metadata awareness
                    var metadata = new AiMetadata
                    {
                        GeneratedAt = DateTime.UtcNow,
                        PromptLength = request.Prompt.Length,
                        Provider = "OllamSharp + Microsoft.Extensions.AI"
                    };

                    //Structured Response
                    var response = new ApiResponse<AiResponseDto>
                    {
                        Success = true,
                        Message = "Ai Response generated Successfully",
                        Data = aiResponse,
                        Metadata = metadata
                    };

                    return Results.Ok(response);

                }
            );

            app.MapPost("/ai/stream",
                async(
                    AiRequest request,
                    IAiService aiService
                ) =>
                {
                    if ( string .IsNullOrWhiteSpace(request.Prompt))
                    {
                        return Results.BadRequest("Prompt is required");
                    }

                    return Results.Stream(async stream =>
                    {
                        

                        await foreach (var chunk in aiService.StreamAsync(request))
                        {

                            await stream.WriteAsync(
                                System.Text.Encoding.UTF8.GetBytes(chunk)
                            );

                            await stream.FlushAsync();

                        }    

                    },
                    "text/plain");
                }
            );
        }
    }
}