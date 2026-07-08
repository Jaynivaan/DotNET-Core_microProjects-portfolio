//gs
namespace Day24.AttentionMeshOS.Models
{
    public sealed record GravityDissolutionCandidate(

        Guid FieldId,
        float AttentionEnergy,
        float Stability,
        float SemanticMass,
        int ParticipantCount,
        TimeSpan FieldAge,
        DateTimeOffset CreatedAt,
        DateTimeOffset LastEvolvedAt,
        bool IsDominantField,
        bool RecentlyReinforced,
        string Reason

        );
}