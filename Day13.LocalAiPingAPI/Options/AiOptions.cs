//gs
namespace Day13.LocalAiPingAPI.Options
{
    //This class stores configurable ai settings
    //
    //this separate options file setting config is a good architectural habit to build
    //when we switch models it can be easily changed in this file.
    //configuration driven systems are easier to scale and maintain.

    public class AiOptions
    {
        //ollama local server url.
        public string BaseUrl { get; set; } = "";

        //model name running inside ollama 
        public string Model { get; set; } = "";

        //global system instructions for the model
        public string SystemPrompt { get; set; } = "";


    }
}