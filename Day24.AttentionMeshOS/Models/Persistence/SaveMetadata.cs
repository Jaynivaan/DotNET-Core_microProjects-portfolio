//gs
using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record SaveMetadata(
        int FormatVersion,
        string RuntimeVersion,
        string PersistenceVersion,
        int SignatureLength,
        int SignatureSchemaVersion,
        int QuantizationVersion,
        DateTimeOffset CreatedAt,
        DateTimeOffset SavedAt,
        string SemanticIdentityMode
        );
}