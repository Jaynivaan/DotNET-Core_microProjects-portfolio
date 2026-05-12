//gs
using Day12.SearchAPI.Metadata;
using Day12.SearchAPI.Responses;
using Day12.SearchAPI.Services;
using System;
using System.Collections.Generic;

namespace Day12.SearchAPI.Endpoints
{
    //Endpoint should remain thin
    //
    //-receive request
    //-call service
    //-return response

    public static class SearchEndpoints
    {
        //
        public static void MapSearchEndpoints(this WebApplication app)
        {
            //Get /search?query=cache&category=backend
            app.MapGet("/search",
                async
                (
                    string  query,
                    string? category,
                    ISearchService searchservice
                    ) =>
                {
                    //Defensive validation
                    if (string.IsNullOrWhiteSpace(query))
                    {
                        return Results.BadRequest
                        (
                            new ApiResponse<string>
                            {
                                Success = false,
                                Message = "Query is required",

                                Metadata = new SearchMetadata
                                {
                                    GeneratedAt = DateTime.UtcNow,
                                    Query = query,
                                    ResultsCount = 0,
                                    Source = "SearchEndpoint"
                                }
                            }
                        );
                    }

                    //call retrieval service
                    var results = await searchservice
                        .SearchAsync(query, category);

                    //structured response
                    var response = new ApiResponse<List<SearchResultDto>>
                    {
                        Success = true,
                        Message = "Search Completed Succussfully yaaay...",
                        Data = results,

                        Metadata = new SearchMetadata
                        {
                            GeneratedAt = DateTime.UtcNow,
                            Query = query,
                            ResultsCount = results.Count,
                            Source = "HybridSearchService"
                        }
                    };
                    return Results.Ok(response);
                }
            );
        }
    }
}