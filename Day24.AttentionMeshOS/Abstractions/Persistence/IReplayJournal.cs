//gs
using System.Collections.Generic;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IReplayJournal
    {
        void Append(ReplayEventRecord replayEvent);

        IReadOnlyList<ReplayEventRecord> GetEvents();

        void Clear();

    }
}