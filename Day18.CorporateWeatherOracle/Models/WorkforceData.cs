//gs
using Microsoft.ML.Data;

namespace Day18.CorporateWeatherOracle.Models
{
    //Records one workforce record from the csv dataset
    public class WorkforceData
    {
        //
        [LoadColumn(0)]//this[LoadColumn(0)] will bind csv column 0 to teh strongly typed c sharp property here..
        public string Company { get; set; } = "";

        [LoadColumn(1)]
        public float Year { get; set; }

        [LoadColumn(2)]
        public float EmployeesStart { get; set; }

        [LoadColumn(3)]
        public float EmployeesEnd { get; set; }

        [LoadColumn(4)]
        public float NewHires { get; set; }

        [LoadColumn(5)]
        public float Layoffs { get; set; }

        [LoadColumn(6)]
        public float NetChange { get; set; }

        [LoadColumn(7)]
        public float HiringRate { get; set; }

        [LoadColumn(8)]
        public float AttritionRate { get; set; }

        [LoadColumn(9)]
        public float RevenueBillionsUsd { get; set; }

        [LoadColumn(10)]
        public float StockPriceChangePct { get; set; }

        [LoadColumn(11)]
        public float GdpGrowthUsPct { get; set; }

        [LoadColumn(12)]
        public float UnemploymentRateUsPct { get; set; }

        [LoadColumn(13)]
        public bool IsEstimated { get; set; }

        [LoadColumn(14)]
        public string ConfidenceLevel { get; set; } = "";

        [LoadColumn(15)]
        public float  DataQualityScore { get; set; }


        //ML target Label
        [LoadColumn(16)]
        public string WeatherLabel { get; set; } = "";
    }
}