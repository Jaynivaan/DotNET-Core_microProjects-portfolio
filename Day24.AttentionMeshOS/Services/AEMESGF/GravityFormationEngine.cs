//gs
using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Options;
using Microsoft.Extensions.Options;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class GravityFormationEngine : IGravityFormationEngine
    {
        private readonly GravityFieldSelectionEngine _selectionEngine;
        private readonly GravityMembershipManager _membershipManager;

    }
}