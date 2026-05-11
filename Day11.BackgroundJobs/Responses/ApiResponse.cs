//gs
using Day11.BackgroundJobs.Metadata;

namespace Day11.BackgroundJobs.Responses
{
    //Generic Response wrapper
    // T means this response can carry any type of data
    //
    //this keep api responses consistent
    //End point can return jobstatus today
    //later it can return reports, alerts, logs etc.

    public class ApiResponse <T>
    {
        //wheather  request succeeded
        public bool Success { get; set; }

        //human readable message for humans if any.. 
        public string Message { get; set; } = "";
        
        // actual payload or  business data
        public T? Data { get; set; }

        //extra cherry on the top context about the response.
        public ResponseMetadata Metadata { get; set; } = new();
    }
}