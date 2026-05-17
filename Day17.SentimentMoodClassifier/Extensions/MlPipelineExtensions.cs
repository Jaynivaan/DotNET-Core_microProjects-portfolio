//gs
using Day17.SentimentMoodClassifier.Services.Interfaces;
using Day17.SentimentMoodClassifier.Services.Trainers;

using Microsoft.Extensions.DependencyInjection;

namespace Day17.SentimentMoodClassifier.Extensions
{
    //all service registrations live here to 
    // avoid crowded program.cs file.

    public static class MlPipelineExtensions
    {
        public static IServiceCollection AddMlPipelineServices(
            this IServiceCollection services)
        {
            services.AddSingleton<IModelTrainer, SentimentModelTrainer>();

            return services;
        }
       
    }
}