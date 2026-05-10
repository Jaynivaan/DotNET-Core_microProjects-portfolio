//gs
using System;

namespace Day10.RateLimiter.Responses
{
    //DTO style Response object.
    //
    //Srp from solid followed here
    //this class only shapes the api responses.

    //Endpoints should not manually build anonymous response objects everywhere.

    //Centralized response  shape
    //Creates cleaner APIs.

    


    public class ApiResponse
    {
       //success, message, timestamp

        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public DateTime Timestamp {  get; set; }

    }
}