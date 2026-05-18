//gs

using Day18.CorporateWeatherOracle.Models;

namespace Day18.CorporateWeatherOracle.Services.Interfaces
{
    public interface IForecastService
    {
        ForecastResponse Predict(WorkforceData input);
    }
}