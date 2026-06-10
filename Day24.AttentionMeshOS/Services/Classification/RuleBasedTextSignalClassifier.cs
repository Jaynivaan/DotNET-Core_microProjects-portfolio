//gs
using Day24.AttentionMeshOS.Models;
using Day24.AttentionMeshOS.Abstractions;
using System.Collections.Generic;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class RuleBasedTextSignalClassifier : ITextSignalClassifier
    {
        public IReadOnlyList<Aspiration> DetectAspirations(string userInput)
        {
            var aspirations= new List<Aspiration>();

            var input = userInput.ToLowerInvariant();

            if (input.Contains("build"))
                aspirations.Add(new Aspiration(Guid.NewGuid(), "Build Something", 0.9));

            if (input.Contains("learn"))
                aspirations.Add(new Aspiration(Guid.NewGuid(), "Learn", 0.8));

            if (input.Contains("Create"))
                aspirations.Add(new Aspiration(Guid.NewGuid(), "Create", 0.8));

            if (input.Contains("job"))
                aspirations.Add(new Aspiration(Guid.NewGuid(), "Employment", 0.9));

            return aspirations;

        }

        public IReadOnlyList<Tendency> DetectTendencies(string userInput)
        {
            var tendencies = new List<Tendency>();

            var input = userInput.ToLowerInvariant();

            if (input.Contains("simple"))
                tendencies.Add(new Tendency(Guid.NewGuid(), "Simplicity", 0.8));

            if (input.Contains("innovative"))
                tendencies.Add(new Tendency(Guid.NewGuid(), "Innovation", 0.9));

            if (input.Contains("research"))
                tendencies.Add(new Tendency(Guid.NewGuid(), "Research", 0.8));

            if (input.Contains("architecture"))
                tendencies.Add(new Tendency(Guid.NewGuid(), "Architecture Thinking", 0.9));

            return tendencies;
        }
    }
}