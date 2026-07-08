//gs
using System;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class BucketMaintenanceService
    {
        private readonly ISemanticQuantizer _quantizer;
        private readonly IBucketRegistry _registry;

        public BucketMaintenanceService(
            ISemanticQuantizer quantizer,
            IBucketRegistry registry)
        {
            _quantizer = quantizer;
            _registry = registry;
        }

        public void RegisterField(
            CandidateFieldRef candidate,
            sbyte[] signature,
            sbyte[] presenceMask)
        {
            SemanticBucketKey bucketKey =
                _quantizer.Quantize(
                    signature, presenceMask);

            _registry.Register(
                bucketKey,
                new SemanticBucketEntry(candidate));
        }

        public void UpdateField(
            CandidateFieldRef candidate,
            sbyte[] previousSignature,
            sbyte[] previousPresenceMask,
            sbyte[] currentSignature,
            sbyte[] currentPresenceMask)
        {
            SemanticBucketKey previousBucketKey = _quantizer.Quantize(
                previousSignature, previousPresenceMask);

            SemanticBucketKey currentBucketKey = _quantizer.Quantize(
                currentSignature, currentPresenceMask);
            
            if ( previousBucketKey.Equals(currentBucketKey))
            {
                _registry.Register(
                    currentBucketKey,
                    new SemanticBucketEntry(candidate));
                return;
            }

            _registry.UnRegister(
                previousBucketKey,
                candidate);

            _registry.Register(
                currentBucketKey,
                new SemanticBucketEntry(candidate));

        }

        //merge field -Bucket orchestration

        public void HandleMerge(
            CandidateFieldRef survivor,
            sbyte[] previousSurvivorSignature,
            sbyte[] previousSurvivorPresenceMask,
            sbyte[] currentSurvivorSignature,
            sbyte[] currentSurvivorPresenceMask,
            CandidateFieldRef retired,
            sbyte[] retiredSignature,
            sbyte[] retiredPresenceMask)
        {
            RemoveField(
                retired,
                retiredSignature,
                retiredPresenceMask);

            UpdateField(
                survivor,
                previousSurvivorSignature,
                previousSurvivorPresenceMask,
                currentSurvivorSignature,
                currentSurvivorPresenceMask);
        }
            
            
            
            
            
        public void RemoveField(
            CandidateFieldRef candidate,
            sbyte[] signature,
            sbyte[] presenceMask)
        {
            SemanticBucketKey bucketKey =
                _quantizer.Quantize(
                    signature, presenceMask);

            _registry.UnRegister(
                bucketKey,
                candidate);
        }
    }
}