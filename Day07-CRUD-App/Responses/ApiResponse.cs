//gs

namespace Day07_CRUD_App.Responses
{
    //Generic response wrapper
    //T means this response can hold any type of data
    public class ApiResponse<T>

    {

        public bool Success { get; set; }

        public string Message { get; set; } = "";

        public T? Data { get; set; }


    }
}