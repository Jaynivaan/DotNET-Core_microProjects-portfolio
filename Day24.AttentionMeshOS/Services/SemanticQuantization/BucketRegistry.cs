//gs

using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using Microsoft.Extensions.Logging;


namespace Day24.AttentionMeshOS.Services
{
    public sealed class BucketRegistry : IBucketRegistry
    {
        private readonly Dictionary<SemanticBucketKey, SemanticBucket> _buckets = new();
        private readonly object _syncLock = new();
        private readonly ILogger<BucketRegistry> _logger;

        public BucketRegistry(
            ILogger<BucketRegistry> logger)
        {
            _logger = logger;
        }

        public void Register(
            SemanticBucketKey bucketKey,
            SemanticBucketEntry entry)
        {
            ArgumentNullException.ThrowIfNull(entry);

            lock(_syncLock)
            {
                if (!_buckets.TryGetValue(bucketKey, out SemanticBucket? bucket))
                {
                    bucket = new SemanticBucket(bucketKey);
                    _buckets[bucketKey] = bucket;
                }

                bucket.InternalEntries.RemoveAll(
                    existing => existing.Candidate.Equals(entry.Candidate));

                bucket.InternalEntries.Add(entry);

                AemEsgfTelemetry.BucketRegistered(
                    _logger,
                    bucketKey.BucketCode,
                    entry.Candidate.FieldId,
                    entry.Candidate.RuntimeIndex);
            }
        }

        public bool UnRegister(
            SemanticBucketKey bucketKey,
            CandidateFieldRef candidate)
        {
            lock(_syncLock)
            {
                if (!_buckets.TryGetValue(bucketKey, out SemanticBucket? bucket))
                {
                    return false;
                }

                int removed = bucket.InternalEntries.RemoveAll(
                    entry => entry.Candidate.Equals(candidate));

                if ( bucket.InternalEntries.Count == 0)
                {
                    _buckets.Remove(bucketKey);
                }

                if ( removed > 0)
                {
                    AemEsgfTelemetry.BucketUnregistered(
                        _logger,
                        bucketKey.BucketCode,
                        candidate.FieldId,
                        candidate.RuntimeIndex);
                } 
                    
                return removed > 0;
            }
        }


        public bool TryGetEntries(
            SemanticBucketKey bucketKey,
            out IReadOnlyList<SemanticBucketEntry> entries)
        {
            lock(_syncLock)
            {
                if (_buckets.TryGetValue(bucketKey, out SemanticBucket? bucket))
                {
                    entries = bucket.InternalEntries.ToArray();
                    return true;
                }
            }
            entries = Array.Empty<SemanticBucketEntry>();
            return false;
        }

        public IReadOnlyList<SemanticBucketSnapshot> GetSnapshots()
        {
            lock (_syncLock)
            {
                List<SemanticBucketSnapshot> snapshots = new();

                foreach (SemanticBucket bucket in _buckets.Values)
                {
                    SemanticBucketEntry[] entries = bucket.InternalEntries.ToArray();

                    snapshots.Add(
                        new SemanticBucketSnapshot(
                            bucket.BucketKey,
                            entries.Length,
                            entries));
                }
                return snapshots;
            }
        }
        
        public void Clear()
        {
            lock (_syncLock)
            {
                _buckets.Clear();
            }
        }
    }
}

