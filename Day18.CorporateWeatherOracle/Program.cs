//gs


using Day18.CorporateWeatherOracle.Extensions;
using Day18.CorporateWeatherOracle.Models;
using Day18.CorporateWeatherOracle.Services.Predictors;
using Day18.CorporateWeatherOracle.Services.Trainers;

using Microsoft.ML;

//ML context
var mlContext = new MLContext();

//csv data path
string dataPath =
    Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "tech_employment_2000_2025.csv"
    );

//trainer
var trainer = new WorkforceModelTrainer();

//train model
var trainedModel = trainer.Train(mlContext, dataPath);

//prediction service
var forecastService = new ForecastService(mlContext, trainedModel);

Console.WriteLine();

//sample workforce signal input

var sampleInput = new WorkforceData
{
    Company = "AMD",

    Year = 2006,

    EmployeesStart = 25000,

    EmployeesEnd = 21000,

    NewHires = 1200,

    Layoffs = 4200,

    NetChange = -3800,

    HiringRate = 4.8f,

    AttritionRate = 18.5f,

    RevenueBillionsUsd = 22.1f,

    StockPriceChangePct = -12.5f,

    GdpGrowthUsPct = 1.4f,

    UnemploymentRateUsPct = 5.9f,

    IsEstimated = false,

    ConfidenceLevel = "High",

    DataQualityScore = 91

};

//predict forecast

var forecast = forecastService.Predict(sampleInput);

//print out fancy card
forecast.PrintOracleCard();

Console.WriteLine();
