//gs
namespace Day06_WeatherApi.Models
{
    //This model represents the weather data 
    // returned from the external weather api.

    public class WeatherResponse
    {
        //city name
        public string City { get; set; } = "";

        //Temperature in celcius
        public double Temperature { get; set; }


        //Weather condition text
        public string Description { get; set; } = "";


        //Humidity percentage
        public int Humidity { get; set; }

        //Wind speed
        public double WindSpeed { get; set; }



    }
}

//this is the Dto of this app 
///why a data transfer object matters?
///because...
///with models its safer, readable, autocompleted, no surprises, compile time checked and maintainable..
///
/// IT IS ALWAYS BEST TO DEFINE THE EXPECTED STRUCTURE BEFOR RUNTIME INSTEAD OF CHAOS
/// 
/// without well defined dto models everything becomes messy dynamic json handling..
///so the models that are strongly typed is defining the data flow ..
///This is me talking to me .. for learning as i am a nowise fool on the c sharp world ..