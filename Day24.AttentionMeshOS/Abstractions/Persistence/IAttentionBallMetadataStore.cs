//gs
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IAttentionBallMetadataStore
    {        

        void Save(AttentionBallMetadata metadata);

        IReadOnlyList<AttentionBallMetadata> GetAll();

        AttentionBallMetadata? GetByAttentionBallId(
            Guid attentionBallId);

        bool Exists(Guid attentionBallId);
    }
}