//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Endpoints
{
    public static class AttentionEndpoints
    {
        public static void MapAttentionEndpoints(this WebApplication app)
        {
            app.MapPost("/attention/shot",(
                AttentionRequest request,
                IAttentionEngine attentionEngine )=>
            {
                var response = attentionEngine.Process(request.UserInput);

                return Results.Ok(response);
                

            });

        }
    }
}