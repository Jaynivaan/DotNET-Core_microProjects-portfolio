//gs
//RequestLoggingMiddleware.cs
//This file is the main brain of our logging system.
//Middleware means code that runs before and after the endpoint is hit.
//In this case, we want to log the request before it hits the endpoint and log the response after it hits the endpoint.

using System;
using System.Diagnostics;// For Stopwatch
using System.Threading.Tasks;
using Day05_LoggingSystem.Models;// For ApiResponse

namespace Day05_LoggingSystem.Middleware
{
    public class  RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware (RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Unique id for this request
            //Helpful when debugging many request
            var requestId = Guid.NewGuid().ToString();

            //stop watch measures how long request takes.
            var stopwatch = Stopwatch.StartNew();

            try 
            {

                //safe logging
                //we log method and path only.
                //we dont log body , password, token or personal data.
                _logger.LogInformation(
                    "Request started | RequestId: {RequestId} | Method: {Method} | Path: {Path}",
                    requestId,
                    context.Request.Method,
                    context.Request.Path                    
                );

                //send request to next middleware /endpoint
                await _next(context);

                stopwatch.Stop();

                _logger.LogInformation(
                    "Request finished | RequestId: {RequestId} | StatusCode: {StatusCode} | TimeMs: {TimeMs}",
                    requestId,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds
                );              
            
            }            
            catch( Exception ex )
            {
                stopwatch.Stop();

                //log real error internally
                //User should not recieve technical error details.

                _logger.LogError(
                    ex, 
                    "Request failed | RequestId: {RequestId} | Path: {Path} | TimeMs: {TimeMs}",
                    requestId,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds
                );
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Something went wrong. Please contact support with RequestId: " + requestId,
                    Data = null
                });

            }
        }
    }
}