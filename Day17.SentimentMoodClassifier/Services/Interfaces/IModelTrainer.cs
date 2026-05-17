//gs
using Microsoft.ML;

namespace Day17.SentimentMoodClassifier.Services.Interfaces
{
    public interface IModelTrainer
    {
        ITransformer TrainModel(
            MLContext mlContext,
            string dataPath
        );
        
    }
}

//very important ml realization  tranining is not equal to prediciton.. which is obvious for a normal party 
// for fool its nice to realize now and then.