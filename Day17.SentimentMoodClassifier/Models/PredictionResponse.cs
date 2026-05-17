//gs

namespace Day17.SentimentMoodClassifier.Models
{
    //Structured response return to console
    //This wraps the prediction into a cleaner response object.

    public class PredictionResponse
    {
        //original incoming text
        public string InputText { get; set; } = "";

        //Predicted Sentiment
        public string Sentiment { get; set; } = "";

        //Confidence score Percentage
        public float Confidence { get; set; }

        //extra message for humanbeings
        public string Message { get; set; } = "";

    }
}