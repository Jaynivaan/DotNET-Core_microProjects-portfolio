//gs


namespace Day20.RequestJourneyAPI.Middleware
{
    public class RequestJourneyMiddleware
    {
        //
        private readonly RequestDelegate _next;

        public RequestJourneyMiddleware(
            RequestDelegate next)
        { _next = next; }

        public async Task InvokeAsync(
            HttpContext context)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(
                $"[Middleware] Incoming Request : {context.Request.Path}");

            Console.ResetColor();


            await _next(context);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(
                $"[Middleware] Response Completed : {context.Response.StatusCode}");

            Console.ResetColor();
        }
    }
}

//fancy Middleware
