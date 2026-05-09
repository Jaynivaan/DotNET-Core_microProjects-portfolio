//gs

using System;

namespace Day09.CachingSystem.Domain
{
    //Domain is the internal truth of the systeml.

    //This file does not know about HTTP.
    //This file does not know about IMemoryCache, Controllers or endpoints.
    //This file only represents the cached data itself.
    //srp from solid principleis implemented here.

    public class CachedItem
    {
        public string Key { get; set; } = "";

        public string Value { get; set; } = "";

        public DateTime CachedAt { get; set; }

    }

}