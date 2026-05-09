//gs
using Day09.CachingSystem.Application.DTOs;
using Day09.CachingSystem.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Day09.CachingSystem.Application.Services
{
    //srp principle from solid is followed here..
    //t his class ownst he data flow for the cache demo 
    // it dont know aboutt he intricate details like HTTP,IMemoryCache 
    // it only coordinates.

    public class CacheDemoService
    {

        private readonly ICacheService _cacheService;

        public CacheDemoService (ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task <CacheResponseDto> GetCacheDemoAsync(string key)
        {
            //simple safety rule 
            // empty keys should not enter the system.
            if (string .IsNullOrWhiteSpace(key))
            {
                return new CacheResponseDto
                {
                    Key = key,
                    Value = "",
                    Source = "Invalid Key",
                    CachedAt = DateTime.UtcNow,
                    TtlSeconds = 0
                };
            }
            //Application layer depends on abstraction: ICacheService
            //it does not care if the real cache is MemoryCache,Redis or HybridCache.
            return await _cacheService.GetOrCreateAsync(key);
        }

    }
}