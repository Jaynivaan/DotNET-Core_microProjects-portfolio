//gs
using System;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class SemanticQuantizer : ISemanticQuantizer
    {
        private const int BlockSize = 8;

        public SemanticBucketKey Quantize(
            sbyte[] signature,
            sbyte[] presenceMask)
        {
            ArgumentNullException.ThrowIfNull(signature);
            ArgumentNullException.ThrowIfNull(presenceMask);

        //  _logger.LogInformation("Quantizer 1: start.");

            if ( signature.Length != presenceMask.Length )
            {
                throw new ArgumentException(
                    "Presence Mask Length must match signature Length.",
                    nameof(presenceMask));
            }

            unchecked
            {
                int bucketCode = 17;
                 
                for ( int blockStart = 0;
                    blockStart < signature.Length;
                    blockStart += BlockSize )
                {
                    int blockCode = 0;

                    int blockeEnd = Math.Min(
                        blockStart + BlockSize,
                        signature.Length);

                    for ( int i = blockStart; i < blockeEnd; i++ )
                    {
                        int ternaryValue = 0;

                        if ( presenceMask[i] != 0 )
                        {
                            ternaryValue = signature[i] switch
                            {
                                < 0 => 1,
                                0 => 2,
                                > 0 => 3
                            };
                        }
                        blockCode = (blockCode * 4) + ternaryValue;
                    }
                    bucketCode = (bucketCode * 31) + blockCode;
                }
                return new SemanticBucketKey(bucketCode);
            }
            
        }
    }
}