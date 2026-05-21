//gs
using Microsoft.Extensions.Options;

using Day21.FeatureflagAPI.Models;
using Day21.FeatureflagAPI.Services.Interfaces;

namespace Day21.FeatureflagAPI.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly FeatureOptions _features;

        public FeatureService(IOptions<FeatureOptions> options)
        {
            _features = options.Value;
        }

        public FeatureOptions GetFeature()
        {
            return _features;
        }
    }
}