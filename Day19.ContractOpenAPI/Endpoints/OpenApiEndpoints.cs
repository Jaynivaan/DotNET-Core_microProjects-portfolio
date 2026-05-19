//gs

using Day19.ContractOpenAPI.Models;
using Day19.ContractOpenAPI.Responses;
using Day19.ContractOpenAPI.Metadata;
using System;

namespace Day19.ContractOpenAPI.Endpoints
{
    public static class OpenApiEndpoints
    {
        public static void MapOpenApiEndpoints(this WebApplication app)
        {
            app.MapGet("/hello", () =>
            "Hello from OpenAPI")
            .WithName("HelloEndpoint")
            .WithSummary("Returns a simple hello greeting")
            .WithDescription("A Tiny endpoint used to demonstrate OpenApi contract discovery.")
            .WithTags("Contract");


            app.MapGet("/openapi-info", () =>
            {
                return new OpenApiInfoResponse
                {
                    Title = OpenApiMetadata.Title,
                    Version = "v1",
                    Purpose = OpenApiMetadata.Purpose
                };
            })
            .WithName("OpenApiInfo")
            .WithSummary("Return OpenApi Project Information")
            .WithDescription("Explains the purpose of this Api Contract demo")
            .WithTags("Contract");


            app.MapGet("/contract-example", () =>
            {

                return new ContractExampleResponse
                {
                    Endpoint = "/hello",
                    Description = "A simple endpoint where Openapi greet the invoker with a hello."
                };
            })
            .WithName("ContractExample")
            .WithOpenApi();
        }
    }
}