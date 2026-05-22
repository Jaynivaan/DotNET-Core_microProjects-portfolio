//gs
namespace Day22.InvoiceToCashLite.Shared
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public T? Data { get; set; }
    }
}