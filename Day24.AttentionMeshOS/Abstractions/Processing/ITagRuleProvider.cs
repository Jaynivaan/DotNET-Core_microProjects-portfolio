//gs
namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ITagRuleProvider
    {
        IReadOnlyDictionary<string, IReadOnlyList<string>> GetMappings();
    }
}