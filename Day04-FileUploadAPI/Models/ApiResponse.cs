//gs

namespace Day04_FileUploadAPI.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }//success or failure of the API call
        public string Message { get; set; } = "";// mesage to provide additional information about the response
        public T? Data { get; set; }// Generic type to hold any type of data
    }
}
