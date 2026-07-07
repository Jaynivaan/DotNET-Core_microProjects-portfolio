//gs
using System;
using System.Collections.Generic;
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityLineageRegistry : IGravityLineageRegistry
    {
        private readonly Dictionary<Guid, GravityFieldLineageRecord> _records = new();
        private readonly object _syncLock = new();

        public void RegisterBirth(
            Guid fieldId,
            DateTimeOffset createdAt)
        {
            lock (_syncLock)
            {
                if (_records.ContainsKey(fieldId))
                {
                    return;
                }

                _records[fieldId] =
                    new GravityFieldLineageRecord(
                        fieldId,
                        new[] { fieldId },
                        Array.Empty<Guid>(),
                        null,
                        createdAt,
                        null,
                        null,
                        "Birth");
            }
        }

        public void RegisterMerge(
            Guid retiredFieldId,
            Guid survivorFieldId,
            DateTimeOffset mergedAt
            )
        {
            lock (_syncLock)
            {
                if (!_records.TryGetValue(
                    retiredFieldId,
                    out GravityFieldLineageRecord? retired))
                {
                    retired =
                        new GravityFieldLineageRecord(
                            retiredFieldId,
                            new[] { retiredFieldId },
                            Array.Empty<Guid>(),
                            null,
                            mergedAt,
                            null,
                            null,
                            "Birth inferred before merge.");
                }

                _records[retiredFieldId] =
                    retired with
                    {
                        MergedIntoFieldId = survivorFieldId,
                        MergedAt = mergedAt,
                        LineageReason = "merged"
                    };

                if (!_records.TryGetValue(
                    survivorFieldId,
                    out GravityFieldLineageRecord? survivor))
                {
                    survivor =
                          new GravityFieldLineageRecord(
                              survivorFieldId,
                              new[] { survivorFieldId },
                              Array.Empty<Guid>(),
                              null,
                              mergedAt,
                              null,
                              null,
                              "Birth inferred as merge survivor");
                }

                _records[survivorFieldId] =
                    survivor with
                    {
                        ParentFieldIds =
                            MergeUniqueAndSort(
                                survivor.ParentFieldIds,
                                retiredFieldId
                                ),

                        OriginFieldIds =
                            MergeUniqueAndSort(
                                survivor.OriginFieldIds,
                                retired.OriginFieldIds),

                        LineageReason = "Merge survivor"
                    };
            }
        }

        public void RegisterDissolution(
            Guid fieldId,
            DateTimeOffset dissolvedAt)
        {
            lock (_syncLock)
            {
                if (!_records.TryGetValue(
                    fieldId,
                    out GravityFieldLineageRecord? record))
                {
                    record =
                        new GravityFieldLineageRecord(
                            fieldId,
                            new[] { fieldId },
                            Array.Empty<Guid>(),
                            null,
                            dissolvedAt,
                            null,
                            null,
                            "Birth inferred before dissolution");
                }

                _records[fieldId] =
                    record with
                    {
                        DissolvedAt = dissolvedAt,
                        LineageReason = "Dissolved"
                    };
            }
        }

        public bool TryGetLineage(
            Guid fieldId,
            out GravityFieldLineageRecord? record)
        {
            lock (_syncLock)
            {
                return _records.TryGetValue(fieldId, out record);
            }
        }

        public GravityFieldLineageState GetState()
        {
            lock (_syncLock)
            {
                if (_records.Count == 0)
                {
                    return new GravityFieldLineageState(
                        Array.Empty<GravityFieldLineageRecord>());
                }

                GravityFieldLineageRecord[] snapshot =
                    new GravityFieldLineageRecord[_records.Count];

                _records.Values.CopyTo(snapshot, 0);

                return new GravityFieldLineageState(snapshot);
            }
        }

        private static IReadOnlyList<Guid> MergeUniqueAndSort(
            IReadOnlyList<Guid> existing,
            Guid valueToAdd)
        {
            for (int i = 0; i < existing.Count; i++)
            {
                if (existing[i] == valueToAdd)
                {
                    return existing;
                }
            }

            Guid[] result = new Guid[existing.Count + 1];

            for (int i = 0; i < existing.Count; i++)
            {
                result[i] = existing[i];
            }

            result[existing.Count] = valueToAdd;

            Array.Sort(result);

            return result;
        }

        public static IReadOnlyList<Guid> MergeUniqueAndSort(
            IReadOnlyList<Guid> sourceA,
            IReadOnlyList<Guid> sourceB)
        {
            HashSet<Guid> unionSet = 
                new HashSet<Guid>(sourceA.Count + sourceB.Count);

            for (int i = 0; i < sourceA.Count; i++)
            {
                unionSet.Add(sourceA[i]);
            }

            for ( int i = 0; i < sourceB.Count; i++)
            {
                unionSet.Add(sourceB[i]);
            }

            Guid[] result = new Guid[unionSet.Count];

            unionSet.CopyTo(result);

            Array.Sort(result);

            return result;
        }
    }
}