//gs
namespace Day12.SearchAPI.Responses
{
    //this class will shape a well defined object as response to the querying user

    //we dont expose db entities to users

    //
    public class SearchResultDto
    {
        public string Title { get; set; } = "";

        public string Category { get; set; } = "";

        public string Source { get; set; } = "";

        //search relevance score 
        //higher the score = more relevant the score.

        public int Score { get; set; }

    }

}