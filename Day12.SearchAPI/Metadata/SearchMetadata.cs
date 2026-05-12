//gs
using System;

namespace Day12.SearchAPI.Metadata
{
    //Metadata describes information about the response.

    //Ai system will benefit from metadata
    //-observability
    //-tracing
    //-ranking explanation
    //-debugging
    //retrieval analysis
    //

    public class SearchMetadata
    {
        //When response was generated
        public DateTime GeneratedAt { get; set; }

        //which query user searched..
        public string Query { get; set; } = "";

        //how many results returned
        public int ResultsCount { get; set; }

        //Which service generated this response
        public string Source { get; set; } = "";


    }
}