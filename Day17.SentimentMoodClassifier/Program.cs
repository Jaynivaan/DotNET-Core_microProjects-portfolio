using Day17.SentimentMoodClassifier.Extensions;
using Day17.SentimentMoodClassifier.Services.Interfaces;
using Day17.SentimentMoodClassifier.Services.Predictors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;

//dependency inj container
var services = new ServiceCollection();

//register ml services
services.AddMlPipelineServices();

//build provider
var serviceProvider = services.BuildServiceProvider();

//MLContext = entrypoint of all ML.NET operations
var mlContext = new MLContext();

//dataset path
//our  dataset is very small 
string dataPath =
    Path.Combine(
        AppContext.BaseDirectory,
        "Data",
        "Sentiments-Data.json"
    );
//obtain trainer from di container
var trainer = serviceProvider.GetRequiredService<IModelTrainer>();

//train model
var trainedModel =
    trainer.TrainModel(
        mlContext, dataPath
        );


//prediction service
var predictor = new PredictionEngineService(
    mlContext,
    trainedModel
    );

Console.WriteLine(
    "=====  Sentiment Mood Classifier   ======"
    );
while( true )
{
    Console.WriteLine();
    Console.Write("Enter Text (or type exit): ");

    string? input = Console.ReadLine();
    
    if (input?.ToLower() == "exit")
    {
        break;
    }

    //predict sentiment
    var result = predictor.Predict(input ?? "");

    Console.WriteLine();
    Console.WriteLine(
        $"Sentiment: {result.Sentiment}"
    );

    Console.WriteLine(
        $"Confidence: {result.Confidence:P2}"
    );

    Console.WriteLine(
        result.Message
    );
}
//