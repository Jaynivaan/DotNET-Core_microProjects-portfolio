//gs
using System;


namespace Day24.AttentionMeshOS.Models
{
    public sealed record AttentionLink(
       Guid FromId,
       Guid ToId,
       string Relationship,
       double Strength,
       DateTimeOffset CreatedAt
        
        );
}