//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionEngine : IAttentionEngine
    {
        private readonly ITextSignalClassifier _classifier;
        private readonly IAttentionStore _store;
        private readonly IPersistenceShotBuilder _shotBuilder;

        public AttentionEngine(
            ITextSignalClassifier classifier,
            IAttentionStore store,
            IPersistenceShotBuilder shotBuilder
            )
        {
            _classifier = classifier;
            _store = store;
            _shotBuilder = shotBuilder;

        }

        public AttentionResponse Process(string userInput)
        {
            var aspirations = _classifier.DetectAspirations(userInput);

            var tendencies = _classifier.DetectTendencies(userInput);

            var attentionBall = new AttentionBall(
                Guid.NewGuid(),
                userInput,
                "Day24.AttentionMeshOS",
                "Avoid raw Memory dumping",
                "Continue building the attention system",
                10,
                DateTimeOffset.UtcNow);
            
            _store.Save( attentionBall);

            var shot = _shotBuilder.Build(
                attentionBall,
                aspirations,
                tendencies
                );

            return new AttentionResponse(
                attentionBall.CurrentAim,
                attentionBall.ActiveProject,
                attentionBall.MustNotForget,
                attentionBall.NextMove,
                aspirations.Select(x=> x.Name).ToList(),
                tendencies.Select(x=> x.Name).ToList(),
                shot.Text
                );
        }
    }
}