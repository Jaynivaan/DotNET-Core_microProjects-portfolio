//gs
//this is the search engine..
//
using Day12.SearchAPI.Config;
using Day12.SearchAPI.Data;
using Day12.SearchAPI.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day12.SearchAPI.Services
{
    //This class contains the actual retrieval logic
    //
    //Endpoint should not know:
    //-ranking 
    //-filtering 
    //-scoring 
    //-EF queries
    //
    //srp only search behaviour logic..

    public class HybridSearchService : ISearchService
    {
        //Database access 
        private readonly SearchDbContext _context;

        //search tuning configs
        private readonly SearchOptions _options;

        //Constructor injection
        //
        //framework give dependencies automatically
        public HybridSearchService 
        (
            SearchDbContext context,
            IOptions<SearchOptions>options
        )
        {
            _context = context;

            //IOptions wrapper contains actual config object
            _options = options.Value;
        }
        //Main retrieval pipeline
        public async Task<List<SearchResultDto>> SearchAsync
        (
            string query,
            string? category
        )
        {
            //Defensive input cleanup.
            query = query.Trim().ToLower();

            //Start query pipeline
            //
            //IQueryable means: query still not executed yet
            //EF build SQL eventually.

            var documentsQuery = _context.Documents.AsQueryable();
            //_context.Documents.AsQueryable() means build query step by step first..not immediatly conjure the all  to the play.

            //Optional category filter.
            if (!string .IsNullOrWhiteSpace(category))
            {
                documentsQuery = documentsQuery
                    .Where(d => d.Category.ToLower() == category.ToLower());
            }

            //Execute SQL Query
            var documents = await documentsQuery.ToListAsync();
            //.ToListAsync() the real sql executed then.

            //Ranking Pipeline
            //here is where score logic happens

            var results = documents
                .Select(document =>
                {
                    int score = 0;

                    //title scoring 
                    if (document.Title.ToLower().Contains(query))
                    {
                        score += _options.TitleWeight;
                    }
                    //content scoring
                    if (document.Content.ToLower().Contains(query))
                    {
                        score += _options.ContentWeight;
                    }

                    //Tag scoring
                    if (document.Tags.ToLower().Contains(query))
                    {
                        score += _options.TagWeight;
                    }

                    return new SearchResultDto
                    {
                        Title = document.Title,
                        Category = document.Category,
                        Source = document.Source,
                        Score = score
                    };
                })

            //Remove Zero relevance Results
            .Where(results => results.Score > 0)

            //Higher score first.
            .OrderByDescending(results => results.Score)

            //defensive result limiting
            .Take(_options.MaxResults)

            .ToList();

        return results;

        }
    }
}