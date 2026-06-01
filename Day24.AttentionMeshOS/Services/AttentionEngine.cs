//gs

using Day24.AttentionMeshOS.Abstractions;
using Day24.AttentionMeshOS.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Day24.AttentionMeshOS.Services
{
    public sealed class AttentionEngine : IAttentionEngine
    {
        private readonly ILogger<AttentionEngine> _logger;
        private readonly ITextSignalClassifier _classifier;
        private readonly IAttentionStore _store;
        private readonly IPersistenceShotBuilder _shotBuilder;
        private readonly IAttentionMeshBuilder _meshBuilder;

        public AttentionEngine(
            ITextSignalClassifier classifier,
            IAttentionStore store,
            IPersistenceShotBuilder shotBuilder,
            IAttentionMeshBuilder meshBuilder,
            ILogger<AttentionEngine> logger 
            )
        {
            _classifier = classifier;
            _store = store;
            _shotBuilder = shotBuilder;
            _meshBuilder = meshBuilder;
            _logger = logger;
        }

        public AttentionResponse Process(string userInput)
        {
            _logger.LogInformation(
                "Processing attention request: {userInput}",
                userInput);

            var aspirations = _classifier.DetectAspirations(userInput);

            var tendencies = _classifier.DetectTendencies(userInput);

            var attentionBall = new AttentionBall(
                Guid.NewGuid(),
                userInput,
                "Day24.AttentionMeshOS",
                "Avoid raw Memory dumping",
                "Continue building the attention system",
                10,
                1.0,
                false,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow);

            _logger.LogInformation(
                "AttentionBall created: {Id}", attentionBall.Id
                );

            _store.Save( attentionBall);

            var mesh = _meshBuilder.Build(attentionBall);

            var relatedContext = mesh.Links
                .Join(
                    mesh.RelatedBalls,
                    link => link.ToId,
                    ball => ball.Id,
                    (link, ball) => new RelatedContextResponse(
                        ball.CurrentAim,
                        Math.Round(link.Strength, 2),
                        Math.Round(ball.AttentionWeight, 2)))
                .ToList();

            _logger.LogInformation(
                "AttentionMesh built with {RelatedCount} related context points", 
                mesh.RelatedBalls.Count);

            var shot = _shotBuilder.Build(
                attentionBall,
                aspirations,
                tendencies
                );

            _logger.LogInformation(
                "PersistenceShot generated Successfully");

            return new AttentionResponse(
                attentionBall.CurrentAim,
                attentionBall.ActiveProject,
                attentionBall.MustNotForget,
                attentionBall.NextMove,
                aspirations.Select(x=> x.Name).ToList(),
                tendencies.Select(x=> x.Name).ToList(),
                //mesh.RelatedBalls
                //    .Select(x => x.CurrentAim).ToList(),
                relatedContext,
                shot.Text
                );
        }
    }
}