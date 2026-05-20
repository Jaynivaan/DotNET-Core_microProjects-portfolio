//gs
using System.Runtime.CompilerServices;

namespace Day20.RequestJourneyAPI.Services
{
    public class RequestJourneyService
    {
        public string GetJourneyMessage()
        {
            return "Request reached service and returned safely.";
        }
    }
}
//This is the worker after the request pass through middleware.

//gs