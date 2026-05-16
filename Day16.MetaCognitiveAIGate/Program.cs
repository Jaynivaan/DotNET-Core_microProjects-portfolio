//gs

using Day16.MetaCognitiveAIGate.Extensions;
using Day16.MetaCognitiveAIGate.Middleware;

var builder = WebApplication.CreateBuilder(args);

//register metacognitive gate services
builder.Services.AddMetaCognitiveGate(
    builder.Configuration
    );

var app  = builder.Build();

//activate Middleware pipeline
app.UseMiddleware<MetaCognitiveMiddleware>();

//simple root endpoint
app.MapGet("/", () => "Meta Cognitive ai Running ");

//start the app

app.Run();