//gs

using System;

namespace Day24.AttentionMeshOS.Models
{
    public sealed record CandidateFingerprint
    {
        public byte[] BlockCodes { get; }

        public int BlockCount { get; }

        public int BlockSize { get; }

        public CandidateFingerprint(
            byte[] blockCodes,
            int blockCount,
            int blockSize
            )
        {
            ArgumentNullException.ThrowIfNull(blockCodes);

            BlockCodes = blockCodes.ToArray();

            BlockCount = blockCount;

            BlockSize = BlockSize;
        }
    }
}