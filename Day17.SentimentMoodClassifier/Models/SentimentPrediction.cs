//gs
namespace Day17.SentimentMoodClassifier.Models
{
    //This cls represent the output from ML.NET  prediction engine.
    //ML.NET fill this values after prediction
    public class SentimentPrediction

    {
        //predicted sentiment result
        public bool PredictedLabel { get; set; }

        //confidence score for each class
        public float Score { get; set; } 

        //probability of prediction
        public float Probability { get; set; }
    }
}

//SemtimentData is where model learn from
//SentimentPrediction is what model say after learning.

