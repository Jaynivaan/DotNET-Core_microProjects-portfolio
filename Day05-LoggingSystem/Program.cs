
//gs
using Day05_LoggingSystem.Middleware;//we will use our custom middleware to log requests.
using Day05_LoggingSystem.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services ( for example, for OpenAPI/Swagger support if needed).
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//======================================================
//STEP 1: Add custom logging middleware to the pipeline.
//=======================================================

//this plugs our middleware into the request processing pipeline. It will be invoked for every incoming HTTP request.
app.UseMiddleware<RequestLoggingMiddleware>();



//======================================================
//STEP 2: Define a simple API endpoint to test our logging.
//=======================================================

//This endpoint will return a simple JSON response. When we hit this endpoint, our logging middleware will log the request details.
app.MapGet("/", () =>
{
    return Results.Ok(new ApiResponse<string>
    {
        Success = true,
        Message = "API is working and request has been logged successfully!",
        Data = "Welcome to the Logging System API!"
    });
});

//Test  endpoint to simulate an error and see how it gets logged.
app.MapGet("/error", () =>
{
    //simulate an error by throwing an exception. This will allow us to see how our logging middle ware handles exceptions and logs error details. 
    throw new Exception("Test exception to demonstrate error logging in the midddleware.");

});

//======================================================
//STEP 3: Run the application.
//=======================================================
app.Run();
