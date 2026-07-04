//gs
using System;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class CandidateFingerprintBuilder
    {
        public CandidateFingerprint Build (
            sbyte[] signature,
            sbyte[] presenceMask,
            int blockSize )
        {
            ArgumentNullException.ThrowIfNull(signature);
            ArgumentNullException.ThrowIfNull(presenceMask);

            if  ( blockSize <= 0 )
            {
                throw new ArgumentOutOfRangeException(nameof(blockSize));

            }

            if ( presenceMask.Length != signature.Length )
            {
                throw new ArgumentException(
                    "Presence mask length must match signature length.",
                    nameof(presenceMask)
                    );
            }

            int blockCount = 
                (signature.Length + blockSize -1 ) / blockSize;

            byte[] blockCodes = new byte[blockCount];

            for ( int block = 0; block < blockCount; block++ )
            {
                int positive = 0;
                int negative = 0;

                int start = block * blockSize;
                int end = Math.Min(start + blockSize, signature.Length);

                for (int i = start; i < end; i++ )
                {
                    if ( presenceMask[i] == 0 )
                    {
                        continue;
                    }

                    if ( signature[i] > 0 )
                    {
                        positive++;
                    }

                    else if ( signature [i] < 0 )
                    {
                        negative++;
                    }
                }

                blockCodes[block] =
                    (byte)(((positive & 0x0F) << 4) |
                    (negative & 0x0F));
            }
            return new CandidateFingerprint(
                blockCodes,
                blockCount,
                blockSize
                );

        }
    }
}