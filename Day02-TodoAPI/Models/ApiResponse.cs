//gs//
//models/apiresponse.cs
namespace Day02_TodoAPI.Models;

//Generic response wrapper
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }

}