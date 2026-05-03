//gs//
namespace Day03_AuthSystem.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";   
        public T? Data { get; set; }// Constructor for success response
    }

   
}