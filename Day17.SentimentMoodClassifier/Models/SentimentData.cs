//gs

namespace Day17.SentimentMoodClassifier.Models
{
    //this represent the model of the training input data 
    // ie the format for examples used for training
    //with in this programme.

    //eg:
    //{
    //"Text" : "I love this programe",
    //"label": true
    //}
    //{
    //"Text" : "this system is terrible",
    //"label": false
    //}
    
    public class SentimentData
    {
        //ml doest learn from rules
        //this ml system learn from labelled examples.
        public string Text { get; set; } = "";

        public bool Label { get; set; }
    }
}