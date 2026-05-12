//gs
using Day12.SearchAPI.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day12.SearchAPI.Services
{
    //this is interface were all the available search services are contracted..
    //endpoint should depend on this and not directly on the HybridSearchService
    //
    //here Dependency inversion is implented
    //O from SOLID is also here as 
    // this allow s extensionability openness.

    public interface ISearchService
    {
        Task<List<SearchResultDto>> SearchAsync(string query, string? category);
        //why task because here async is in play 
        //why ? after category because its optional

    }
}