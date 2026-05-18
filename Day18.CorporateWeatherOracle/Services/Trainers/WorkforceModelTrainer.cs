//gs

using Day18.CorporateWeatherOracle.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using Day18.CorporateWeatherOracle.Extensions;

namespace Day18.CorporateWeatherOracle.Services.Trainers
{

    //build and trains multiclass ml model

    public class WorkforceModelTrainer
    {
        public ITransformer Train(
            MLContext mlContext,
            string DataPath )
        {
            //load csv data set 

            var rows = File.ReadAllLines( DataPath )
                .Skip( 1 )
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line
                =>
                {
                    string Clean(string value) => value.Trim().Trim('"');
                    var p = line.Split(',');

                    var data = new WorkforceData
                    {
                        Company = Clean(p[0]),
                        Year = float.Parse(Clean(p[1])),
                        EmployeesStart = float.Parse(Clean(p[2])),
                        EmployeesEnd = float.Parse(Clean(p[3])),
                        NewHires = float.Parse(Clean(p[4])),
                        Layoffs = float.Parse(Clean(p[5])),
                        NetChange = float.Parse(Clean(p[6])),
                        HiringRate = float.Parse(Clean(p[7])),
                        AttritionRate = float.Parse(Clean(p[8])),
                        RevenueBillionsUsd = float.Parse(Clean(p[9])),
                        StockPriceChangePct = float.Parse(Clean(p[10])),
                        GdpGrowthUsPct = float.Parse(Clean(p[11])),
                        UnemploymentRateUsPct = float.Parse(Clean(p[12])),
                        IsEstimated = bool.Parse(Clean(p[13])),
                        ConfidenceLevel = Clean(p[14]),
                        DataQualityScore = float.Parse(Clean(p[15]))
                    };
                    data.WeatherLabel = ForecastExtensions.CreateWeatherLabel(data);

                    return data;
                })
                .ToList();

            IDataView dataView = mlContext.Data.LoadFromEnumerable(rows);
            //pipeline
            var pipeline =

                //convert weather Label into ML key
                mlContext.Transforms.Conversion
                    .MapValueToKey(
                        outputColumnName: "Label",
                        inputColumnName:
                            nameof(WorkforceData.WeatherLabel)
                    )

                //Convert Company into numeric encoding
                .Append(
                    mlContext.Transforms.Categorical
                        .OneHotEncoding(
                            outputColumnName: "CompanyEncoded",
                            inputColumnName: nameof(WorkforceData.Company)
                        )
                )

                //normalize numeric features
                .Append(
                    mlContext.Transforms.NormalizeMinMax(
                        outputColumnName: nameof(WorkforceData.Layoffs),
                        inputColumnName: nameof(WorkforceData.Layoffs)
                    )
                )


                //combine features into single vector
                .Append(
                    mlContext.Transforms.Concatenate(
                        "Features",

                        "CompanyEncoded",

                        nameof(WorkforceData.Year),
                        nameof(WorkforceData.NewHires),
                        nameof(WorkforceData.Layoffs),
                        nameof(WorkforceData.NetChange),
                        nameof(WorkforceData.HiringRate),
                        nameof(WorkforceData.AttritionRate),
                        nameof(WorkforceData.RevenueBillionsUsd),
                        nameof(WorkforceData.StockPriceChangePct),
                        nameof(WorkforceData.GdpGrowthUsPct),
                        nameof(WorkforceData.UnemploymentRateUsPct),
                        nameof(WorkforceData.DataQualityScore)
                    )
                )

                //train Multiclass Model
                .Append(
                    mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy()
                )

                //convert prediction key back into label

                .Append(
                    mlContext.Transforms.Conversion
                        .MapKeyToValue(
                            outputColumnName: "PredictedLabel"
                        )
                );

            //TrainModel
            ITransformer model = pipeline.Fit(dataView);

            return model;

        }
    }
}

//each append literally modifies the mathematical transformation pipeline,,
//raw=>encodetext=>normalize signals=> combinedimensions=>learn statistical boundaries =>predict