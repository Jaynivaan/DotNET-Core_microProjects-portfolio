//gs
using Day12.SearchAPI.Metadata;

namespace Day12.SearchAPI.Responses
{
    //Generic Api Response Wrapper
    //
    //T denotes this class capable to carry different kinds of data.
    //eg today ApiResponse<List<SearchResultDto>>
    //but tomorrow it delivers ApiResponse<UserDto>, ApiResponse<ResponseDto> 
    //same outer shell different inner payload we pack..

    public class ApiResponse<T>
    {
        //success?
        public bool Success { get; set; }

        //message for humanbeings
        public string Message { get; set; } = "";

        //aCTUAL business/search data
        public T? Data { get; set; }

        //Context about how the response was produced
        public SearchMetadata Metadata { get; set; } = new();

    }
}