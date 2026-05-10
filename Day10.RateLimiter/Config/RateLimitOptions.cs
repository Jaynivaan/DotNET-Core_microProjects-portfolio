//gs

namespace Day10.RateLimiter.Config
{
    //SRP from solid
    //This class only stores Ratelimiting configuration values.

    //Configuration driven Architecture:
    //values can change from appsettings.json
    //without changing middleware code.

    public class RateLimitOptions
    {
        //How many request are allowed inside the configured window?
        public int PermitLimit { get; set; }

        //Time window in seconds
        //Example: 5 request in every 10 seconds

        public int WindowSeconds { get; set; }

        //How many request can wait in queue
        public int QueueLimit { get; set; }

    }
}