//gs
using Day09.CachingSystem.Application.DTOs;
using System.Threading.Tasks;

namespace Day09.CachingSystem.Application.Interfaces
{

    //Dependency inversion principle is followed here from SOLID
    //ie higher level code is depending on the abstraction not on the concrete infrastructure code.


    //This interface is a contract adapter for loosely coupling higher level code and lower level concrete code.
    // this says any cache service if it is a cacheservice it should possess these behaviours.. 
    //today implementation may use IMemoryCache
    //tomorrow  it may be using Redis or HybridCache..
    // the api layer should not be bothered.

    public interface ICacheService
    {
        //Async Ready design.
        //Even if memory Cache is fast today.
        //future  cache systems like redis may involve network calls .

        //Asyn syntax

        Task<CacheResponseDto> GetOrCreateAsync(string key);
    }



}