//gs


using Day06_WeatherApi.Models;
using System.Text.Json;
using Day06_WeatherApi.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Day06_WeatherApi.Services
{
    //This service handles communication
    //with the external weather api

    public class WeatherService : IWeatherService
    {
        //Factory used to create HttpClient safely.
        private readonly IHttpClientFactory _httpClientFactory;

        //Configuration values loaded from appSettings.json
        private readonly WeatherApiOption _options;

        //constructor injection
        public WeatherService
        (
        
            IHttpClientFactory httpClientFactory,
            IOptions<WeatherApiOption> options
            
        )
        {
            _httpClientFactory = httpClientFactory;

            //Extract actual configuration values
            _options = options.Value;
        }
        
        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            try
            {
                //create httpClient from Factory
                var Client = _httpClientFactory.CreateClient();// we are not using old new HttpClient() instead using CreateClient()

                //Build external api url  safely using configuration
                var url = $"{_options.BaseUrl}/{city}?format=j1";

                //send request to external api
                var response = await Client.GetAsync(url);

                //check if api call failed
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                //read json response as string 
                var json = await response.Content.ReadAsStringAsync();

                //for now just print raw json for learning purposes
                Console.WriteLine(json);

                //Parse json safely
                using var document = JsonDocument.Parse(json);

                //Root of json object
                var root = document.RootElement;

                //Extract current weather array first item
                var currentCondition = root
                    .GetProperty("current_condition")[0];

                //Extract values safely
                var temperature =
                    currentCondition.GetProperty("temp_C").GetString();

                var description =
                    currentCondition
                    .GetProperty("weatherDesc")[0]
                    .GetProperty("value")
                    .GetString();

                var humidity =
                    currentCondition.GetProperty("humidity").GetString();

                var windSpeed =
                    currentCondition.GetProperty("windspeedKmph").GetString();

                //Return Parsed data
                return new WeatherResponse
                {
                    City = city,

                    Temperature =
                        double.TryParse(temperature, out var temp)
                        ? temp
                        : 0,

                    Description = description ?? "Unknown",

                    Humidity =
                        int.TryParse(humidity, out var hum)
                        ? hum
                        : 0,
                    WindSpeed =
                        double.TryParse(windSpeed, out var wind)
                        ? wind
                        : 0

                };              
            }
            catch
            {
                //Never expose internal technical errors directly
                return null;
            }

        }
    }

}