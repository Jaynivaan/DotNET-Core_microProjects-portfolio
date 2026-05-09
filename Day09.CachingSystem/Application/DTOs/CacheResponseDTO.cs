//gs

using System;

namespace Day09.CachingSystem.Application.DTOs
{
    //srp principle from SOLID is followed here
    //that one responsibility is shape the data we return to the user.
    //
    //Dto is the data transfer object..
    //we dont return domain cached item directly
    //why ? 
    //because that is internal business
    // dto create a structured railed track for data to travel comfortable between system and user..
    //ie dto shape the data we return to user.

    public class CacheResponseDto
    {
        public string Key { get; set; } = "";

        public string Value { get; set; } = "";



        //show if data come from cache or if its newly generated..
        //Example Cache Hit or Cache Miss

        public string Source { get; set; } = "";

        public DateTime CachedAt { get; set; } 

        public int TtlSeconds { get; set; }

    }

}