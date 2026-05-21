//gs

using Day21.FeatureflagAPI.Models;

namespace Day21.FeatureflagAPI.Services.Interfaces
{
    public interface IFeatureService
    {
        FeatureOptions GetFeature();
    }
}