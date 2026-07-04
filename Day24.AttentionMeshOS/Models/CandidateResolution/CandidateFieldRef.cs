//gs

namespace Day24.AttentionMeshOS.Models
{
    public readonly record struct CandidateFieldRef(
        Guid FieldId,
        int RuntimeIndex
        );
}