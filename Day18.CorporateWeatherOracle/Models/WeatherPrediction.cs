//gs

using Microsoft.ML.Data;

namespace Day18.CorporateWeatherOracle.Models
{
    //raw ml prediction output
    public class WeatherPrediction
    {
        //predicted weather label.
        [ColumnName("PredictedLabel")]
        public string PredictedWeather { get; set; } = "";


        //confidencescores for all classes.
        public float[] Score { get; set; } = [];//thid array type of syntax only for multiclass yeaterday project sentiment classifier was a single class .

    }
}