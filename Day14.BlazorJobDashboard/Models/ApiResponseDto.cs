//gs
namespace Day14.BlazorJobDashboard.Models
{
    public class ApiResponseDto<T>
    {
        public bool Succes { get; set; }

        public string Message { get; set; } = "";

        public T? Data { get; set; }
    }
}