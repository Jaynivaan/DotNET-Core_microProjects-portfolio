//gs

using System.Threading.Tasks;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionAnchorService
    {
        bool ShouldCreateAnchor(string userInput);
         
    }
}
