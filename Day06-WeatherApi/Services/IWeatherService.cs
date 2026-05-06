//gs
using Day06_WeatherApi.Models;


namespace Day06_WeatherApi.Services
{
    //this interface defines the contract 
    //for wether related operations..

    public interface IWeatherService
    {
        //fetch weather information asynchronously using city name
        Task<WeatherResponse?> GetWeatherAsync(string city);
    }
}


//Task<WeatherResponse?> 
//means async operation
//this is not dead waiting it will eventually return response..
//may return null 
//this async / await is important modern backend pattern 
//because external apis are network operations  can be slow or unpredictable we dont want thread waiting doing nothing..
//so async/await is the superhero that efficiently acts even achieve scalability..
//threads get blocked but async remains responsive..