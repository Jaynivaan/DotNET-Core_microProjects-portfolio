//gs

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ITextSimilarityService
    {
        double CalculateSimilarity(
            string firstText,
            string secondText);
    }
}