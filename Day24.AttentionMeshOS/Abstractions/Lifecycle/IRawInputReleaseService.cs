//gs
using System;
using Day24.AttentionMeshOS.Models;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface IRawInputReleaseService
    {
        DeleteResponse Release(Guid rawInputId);

        DeleteResponse ReleaseAll(bool Confirm);

        DeleteResponse CascadeRelease(Guid rawInputId, bool confirm);

        DeleteResponse CascadeReleaseAll(bool confirm);

    }
}