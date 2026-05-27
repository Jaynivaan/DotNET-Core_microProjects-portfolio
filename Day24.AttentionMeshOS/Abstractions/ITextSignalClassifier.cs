//gs

using Day24.AttentionMeshOS.Models;
using System.Collections.Generic;

namespace Day24.AttentionMeshOS.Abstractions
{
    public interface ITextSignalClassifier
    {
        IReadOnlyList<Aspiration> DetectAspirations(string userInput);

        IReadOnlyList<Tendency> DetectTendencies (string userInput);
    }
}