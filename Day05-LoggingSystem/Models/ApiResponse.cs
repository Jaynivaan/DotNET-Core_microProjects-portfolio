//gs
//ApiResponse.cs
//this is common response wrapper.
//it keeps all API responses in the same clean shape.

using System.Web;

namespace Day05_LoggingSystem.Models
{
    public class ApiResponse<T>
    {
        //tells whether request succeded or failed
        public bool Success { get; set; }

        //human readable message
        public string Message { get; set; } = "";

        //actual returned data
        //T means this  can hold string , object, list etc.
        public T? Data { get; set; }
    }
}
