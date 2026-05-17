//gs
using Microsoft.ML;
using Day17.SentimentMoodClassifier.Models;

namespace Day17.SentimentMoodClassifier.Services.Predictors
{
    //srp : resp only for prediction training is done else somewhere inside (SentimentModelTrainer.cs)
    public class PredictionEngineService
    {
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public PredictionEngineService(
            MLContext mlContext,
            ITransformer trainedModel)
        {
            _predictionEngine = mlContext.Model.CreatePredictionEngine
                <SentimentData, SentimentPrediction>(trainedModel);
        }

        public PredictionResponse Predict (string inputText)
        {
            var input = new SentimentData
            {
                Text = inputText
            };

            var prediction = _predictionEngine.Predict (input);

            var sentiment = prediction.PredictedLabel ? "Positive" : "Negetive";

            return new PredictionResponse
            {
                InputText = inputText,
                Sentiment = sentiment,
                Confidence = prediction.Probability,
                Message = $"Model classified this text as {sentiment}."
            };
        }
    }
}