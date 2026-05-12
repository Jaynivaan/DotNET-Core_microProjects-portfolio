//gs

using System;

namespace Day12.SearchAPI.Models
{
    // This class represents one searchable document in out system.

    //Body-Part ThinkingMethodology
    // This is like one memory cell inside the search body.
    //
    //SRP: Its ony responsibility is to represent stored searchable data.

    public class SearchDocument
    {
        //id,title, content, category,tags, source, createdat
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public string Category { get; set; } = ""; 

        public string Tags { get; set; } = "";

        public string Source { get; set; } = "";

        public DateTime Created { get; set; }


    }
}