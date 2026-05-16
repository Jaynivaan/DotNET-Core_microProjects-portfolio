//gs
using System.Collections;
using System.IO;
using System.Net.Cache;
using System.Text.Json;
using System.Threading.Tasks;
using Day16.MetaCognitiveAIGate.Models;

using Day16.MetaCognitiveAIGate.Services.Interfaces;

namespace Day16.MetaCognitiveAIGate.Middleware
{
    public class MetaCognitiveMiddleware
    {
        private readonly RequestDelegate _next;

        public MetaCognitiveMiddleware(RequestDelegate next)
        {  _next = next; }

        public async Task InvokeAsync(
            HttpContext context,
            IGateDecisionService decisionService,
            IEnumerable<IRoutingStrategy> strategies )
        {
            //only inspect this endpoint
            if (context.Request.Path != "/inspect")
            {
                await _next(context);
                return;
            }

            //read incoming req body
            using var reader =
                new StreamReader(context.Request.Body);

            var body = await reader.ReadToEndAsync();

            //deserialize incomign message
            var request =
                JsonSerializer.Deserialize<PromptInspectionRequest>(
                    body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            //null protection
            if (request == null)
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(
                   "Invalid request."
                );
                return;
            }

            //gate decisoin
            var decision = decisionService.Inspect(request);

            //choose first routing strategy for now
            var route =
                strategies.First()
                .Route(decision);

            //response
            var response =
                new ApiResponse<object>
                {
                    Success = decision.Accepted,

                    Message = decision.Reason,

                    Data = new
                    {
                        Decision = decision,
                        Route = route
                    },

                    Metadata = new GateMetadata
                    {
                        PromptLength = request.Prompt.Length,

                        Source = request.Source

                    }
                };

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}