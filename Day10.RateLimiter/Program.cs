//gs
using System.Threading.RateLimiting;
using Day10.RateLimiter.Config;
using Day10.RateLimiter.Endpoints;
using Day10.RateLimiter.Responses;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//-------------------------------------------
//   Configuration Binding
//-------------------------------------------
//bind appsettings.json where we added the custom  settings section into strongly typed configuration object.

var rateLimitOptions = new RateLimitOptions();

builder.Configuration
    .GetSection("RateLimiting")
    .Bind(rateLimitOptions);

//-----------------------------------------------------
// Rate limiter Middleware
//--------------------------------

//Modern defensive backend engineering 
//Middle ware sits in  request Pipeline
//Every request flows through Middleware.

//here we create  a named policy called storm

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("storm", limiterOptions =>
    {
        limiterOptions.PermitLimit = rateLimitOptions.PermitLimit;
        limiterOptions.Window =
            TimeSpan.FromSeconds(rateLimitOptions.WindowSeconds);
        limiterOptions.QueueLimit = rateLimitOptions.QueueLimit;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    //------------------------------------------------------
    //  HTTP 429 response
    //---------------------------------------------------

    // Instead of ugly default response we return well structured DTO

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;

        await context.HttpContext.Response.WriteAsJsonAsync(
            new ApiResponse
            {
                Success = false,
                Message = "Too many Requests..Storm shield Activated ",
                Timestamp = DateTime.UtcNow
            },
            cancellationToken: token
        );
    };

});

//next build app 
var app = builder.Build();

//-----------------------------------------
// Middleware Pipeline
//--------------------------------------

//middle ware order matters
// request flow through pipeline in a certain order.

app.UseRateLimiter();

//Endpoint mapping
app.MapShieldEndpoints();

//run the app

app.Run();


