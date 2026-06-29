//gs
namespace Day24.AttentionMeshOS.Models
{
    /// <summary>
    /// this defines the state of determinestic life for an emergentSemanticGravity field.
    /// all state transitions must be governed by explicit invariant checks..
    /// </summary>
    public enum GravityFieldLifecycleState : byte
    {
        //unallocated or inactive field boundary
        Dormant = 0,

        //initial semantic evidence appeared but field not stable yet
        Emerging = 1,

        //field reached structural stability through repeated semantic reinforcement
        Stable = 2,

        //field has reached strong semantic mass, energy, or influence.
        Dominant = 3,

        //field explicitly marked as weakening  or failing formation requirements.
        Dissipating = 4

    }

}