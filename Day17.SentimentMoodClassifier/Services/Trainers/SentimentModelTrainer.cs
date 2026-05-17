//gs

using Day17.SentimentMoodClassifier.Models;
using Day17.SentimentMoodClassifier.Services.Interfaces;
using Microsoft.ML;
using System.Text.Json;

namespace Day17.SentimentMoodClassifier.Services.Trainers
{
    //Responsible only for training the sentiment model.
    public class SentimentModelTrainer : IModelTrainer
    {
        public ITransformer TrainModel (
            MLContext mlContext,
            string dataPath)
        {
            //load training dataset
            var json = File.ReadAllText (dataPath);
            
            var trainingRows =
                JsonSerializer.Deserialize<List<SentimentData>>(json)
                ?? new List<SentimentData>();
            
            IDataView trainingData = mlContext.Data.LoadFromEnumerable(trainingRows);

            //ML Pipeline
            //Text => numericalFeatures => trainer
            var pipeline =
                mlContext.Transforms.Text
                    .FeaturizeText(
                        outputColumnName: "Features",
                        inputColumnName: nameof(SentimentData.Text)
                    )

                    .Append(
                        mlContext.BinaryClassification.Trainers
                            .SdcaLogisticRegression(
                                labelColumnName:
                                    nameof(SentimentData.Label),

                                featureColumnName: "Features"
                            )
                    );
            ///
            ///this single piple line contains one of the deepest ML Concepts
            ///ie, HUmantext => FeatureExtraction => NumericalVectors => Statistical trainer => Learned mathematical model.
            //

            //train model
            ITransformer model =
                pipeline.Fit(trainingData);
            return model;
        }
    }
}