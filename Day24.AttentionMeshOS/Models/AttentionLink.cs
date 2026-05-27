//gs
using System;


namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionLink(
       Guid FormId,
       Guid ToId,
       string Relationship,
       double Strength
        );
}