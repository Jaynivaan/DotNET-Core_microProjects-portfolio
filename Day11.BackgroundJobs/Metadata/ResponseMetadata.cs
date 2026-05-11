//gs
using System;

namespace Day11.BackgroundJobs.Metadata
{
    //metadata means the information about the response
    //not the actual buisiness payload itself..
    /// <summary>
    /// the relevance of metadata i realize within a system is the the metadata tell the system about the state of system itself.., build context around.. or  awareness orientation step..
    /// btw awareness is good to have if anyone want his/her  system to become intelligent among stupid compooters. metadata is a good architectural habit to have on this ai era.
    /// </summary>
    public class ResponseMetadata
    {
        //when response was generated
        public DateTime GeneratedAt { get; set; }

        //machine/server/environment name
        public string Environment { get; set; } = "";

        //simple api version tracking 
        public string Version { get; set; } = "";
    }
}