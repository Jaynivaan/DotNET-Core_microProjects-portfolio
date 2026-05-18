//gs


using Day18.CorporateWeatherOracle.Models;

namespace Day18.CorporateWeatherOracle.Extensions
{

    
   
    public static class ForecastExtensions
    {
        public static string CreateWeatherLabel(WorkforceData data)
        {
            if (data.Layoffs > 3000 ||
                data.AttritionRate > 12 ||
                data.NetChange < -2000)
            {
                return "Thunderstorm";
            }

            if (data.Layoffs > 1000 ||
                data.AttritionRate > 7 || data.NetChange < 0)
            {
                return "Cloudy";
            }
            return "ClearSky";
        }


        //converts forecast into console..
        //consolecolor
        public static ConsoleColor GetForecastColor(this ForecastResponse response)
        {
            return response.Forecast switch
            {
                "ClearSky" => ConsoleColor.Green,

                "Cloudy" => ConsoleColor.Yellow,

                "Thunderstorm" => ConsoleColor.Red,

                _ => ConsoleColor.White
            };
        }

        //forecast card

        public static void PrintOracleCard (this ForecastResponse response)
        {
            Console.ForegroundColor = response.GetForecastColor();

            Console.WriteLine();
            Console.WriteLine(
                "=========================================================================================="
                );
            Console.WriteLine(
                "||                                                                                       ||"
                );
            Console.WriteLine(
                "||                             CORPORATE-WEATHER-ORACLE                                  ||"
                );
            Console.WriteLine(
                "||                                                                                       ||"
                );
            Console.WriteLine(
                "=========================================================================================="
                );

            Console.WriteLine(
                $"Company   : {response.Company}"
                );

            Console.WriteLine(
                $"Year      : { response.Year}" 
                );
            Console.WriteLine(
                $"Forecast   : {response.Forecast}"
                );
            Console.WriteLine(
                $"Confidence   : {response.Confidence:P2}"
                );
            Console.WriteLine(
                $"Meaning   : {response.Meaning}"
                );
            Console.WriteLine(
                "=========================================================================================="
                );
            Console.ResetColor();
        }
    }
}