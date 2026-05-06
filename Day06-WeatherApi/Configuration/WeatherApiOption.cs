//gs
namespace Day06_WeatherApi.Configuration
{
    //This class stores weather api configuration values.
    //This values will come from appsettings.json
    public class WeatherApiOption
    {
        //base url of external weather api
        public string BaseUrl { get; set; } = "";

        //Api Key for Authentication
        public string ApiKey { get; set; } = "";
    }
}
