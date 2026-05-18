//gs
using Day18.CorporateWeatherOracle.Models;

using Day18.CorporateWeatherOracle.Services.Interfaces;

using Microsoft.ML;

namespace Day18.CorporateWeatherOracle.Services.Predictors
{
    public class ForecastService : IForecastService
    {
        private readonly PredictionEngine<WorkforceData, WeatherPrediction> _predictionEngine;

        public ForecastService (
            MLContext mlContext,
            ITransformer trainedModel )
        {
            _predictionEngine = mlContext.Model
                                    .CreatePredictionEngine<WorkforceData, WeatherPrediction>(trainedModel);
        }

        public ForecastResponse Predict (WorkforceData input)
        {
            //run prediction
            var prediction = _predictionEngine.Predict(input);

            //high confidence score
            float confidence = prediction.Score.Max();

            //human readable meaning 
            string meaning = prediction.PredictedWeather switch
            {
                "ClearSky" => "Workforce  stability signals are healthy",

                "Cloudy" => "Mixed hiring and firing signals detected",

                "Thunderstorm" => "High rate of firing , workplace instability detected.",

                _ => "Unknown workforce climate."
            };

            return new ForecastResponse
            {
                Company = input.Company,

                Year = input.Year,

                Forecast = prediction.PredictedWeather,

                Meaning = meaning,

                Confidence = confidence
            };
        }


    }
}