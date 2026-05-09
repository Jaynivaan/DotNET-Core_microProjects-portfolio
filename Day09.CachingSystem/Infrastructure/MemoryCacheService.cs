//gs
using Day09.CachingSystem.Application.DTOs;
using Day09.CachingSystem.Application.Interfaces;
using Day09.CachingSystem.Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Day09.CachingSystem.Infrastructure
{
    //Dependency inversion principle is implemented here.
    //this class implement ICacheService.
    //The api layer will depend on ICacheService not directly on MemoryCacheService.
    //
    //srp from solid is also applicable to this class.
    // this class has only one reason to change..
    //handle memorycache storage and retrieval.

    //This is infrastructure as it know about the IMemoryCache.
    //Application and domain should not be knowing this much details..


    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        private const int TtlSeconds = 30;

        public MemoryCacheService (IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<CacheResponseDto> GetOrCreateAsync(string key)
        {
            //TryGetValue checks if this key already exists in cache.
            //if found as key existing this is called cache Hit..
            if(_memoryCache.TryGetValue(key, out CachedItem? cachedItem) && cachedItem is not null)
            {
                return Task.FromResult(new CacheResponseDto
                {
                    Key = cachedItem.Key,
                    Value = cachedItem.Value,
                    Source = "Cache Hit",
                    CachedAt = cachedItem.CachedAt,
                    TtlSeconds = TtlSeconds

                });
            }

            //if key is not found , then this is called cache miss
            //here we create new data and store it in cache.

            var newItem = new CachedItem
            {
                Key = key,
                Value = $"Generated Value for {key} at {DateTime.UtcNow}",
                CachedAt = DateTime.UtcNow
            };

            _memoryCache.Set(
                key,
                newItem,
                TimeSpan.FromSeconds(TtlSeconds)
            );
            return Task.FromResult(new CacheResponseDto
            {
                Key = newItem.Key,
                Value = newItem.Value,
                Source = "Cache Miss",
                CachedAt = newItem.CachedAt,
                TtlSeconds = TtlSeconds
            });
        }
    }
}