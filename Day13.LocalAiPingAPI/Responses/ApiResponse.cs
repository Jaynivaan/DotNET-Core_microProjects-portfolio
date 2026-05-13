//gs

namespace Day13.LocalAiPingAPI.Responses
{
    //this generic class for api response wrapper can be used for all types of data ..
    public class ApiResponse<T>
    {
        //success
        public bool Success { get; set; }

        //human readable message
        public string Message { get; set; } = "";

        //payload
        public T? Data { get; set; }

        //metadata object
        public object? Metadata { get; set; }

    }
}