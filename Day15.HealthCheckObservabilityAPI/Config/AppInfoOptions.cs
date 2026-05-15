//gs
namespace Day15.HealthCheckObservabilityAPI.Config
{
    //Configuration values from appsettings.json bind here..
    public class AppInfoOptions
    {
        public string AppName { get; set; } = "";

        public string Version { get; set; } = "";

        public string EnvironmentName { get; set; } = "";

        //
    }
}