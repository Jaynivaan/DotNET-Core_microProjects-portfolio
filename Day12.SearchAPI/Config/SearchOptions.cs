//gs
namespace Day12.SearchAPI.Config
{
    //this class stores search behaviour settings
    //why we are using this file

    //because the ranking numbers should not be burried inside the service logic.
    //
    // if tomorrow we  want title match to be stronger then , just easily re configure this file.. 
    // not to rebuild the and rewire search engine..
    //srp : this file only stores search engine settings..
    public class SearchOptions
    {
        //Points added when query appears in title.
        public int TitleWeight { get; set; } = 5;

        //points added when query appears in content.
        public int ContentWeight { get; set; } = 3;

        //points added when query appears in tags
        public int TagWeight { get; set; } = 4;

        //Maximum number of results returned.
        public int MaxResults { get; set; } = 10;
    }
}