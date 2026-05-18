//gs
namespace Day18.CorporateWeatherOracle.Models
{
    //human readable forcast response
    public class ForecastResponse
    {
        public string Company { get; set; } = "";

        public float Year { get; set; }

        public string Forecast { get; set; } = "";

        public string Meaning { get; set; } = ""; 

        public float Confidence { get; set; } 

    }
}