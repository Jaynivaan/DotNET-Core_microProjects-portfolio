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
        private readonly IAttentionMeshBuilder _meshBuilder;

        public AttentionEngine(
            ITextSignalClassifier classifier,
            IAttentionStore store,
            IPersistenceShotBuilder shotBuilder,
            IAttentionMeshBuilder meshBuilder
            )
        {
            _classifier = classifier;
            _store = store;
            _shotBuilder = shotBuilder;
            _meshBuilder = meshBuilder;
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

            var mesh = _meshBuilder.Build(attentionBall);


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
                mesh.RelatedBalls
                    .Select(x => x.CurrentAim).ToList(),
                shot.Text
                );
        }
    }
}