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

        private readonly IAttentionStore _store;
        private readonly IRawAttentionInputStore _rawInputStore;
        private readonly IAttentionBallMetadataStore _metadataStore;
        
        private readonly IInputProcessingOrchestrator _inputProcessingOrchestrator;

        private readonly ITextSignalClassifier _classifier;

        private readonly IAttentionBallMetadataFactory _metadataFactory;
        private readonly IPersistenceShotBuilder _shotBuilder;
        private readonly IAttentionMeshBuilder _meshBuilder;
        private readonly IAttentionAnchorService _anchorService;

        public AttentionEngine(
            ILogger<AttentionEngine> logger,
            
            IRawAttentionInputStore rawInputStore,
            IAttentionStore store,
            IAttentionBallMetadataStore metadataStore,
            
            IInputProcessingOrchestrator inputProcessingOrchestrator,

            ITextSignalClassifier classifier,
            IAttentionBallMetadataFactory metadataFactory,
            IPersistenceShotBuilder shotBuilder,
            IAttentionMeshBuilder meshBuilder,
            IAttentionAnchorService anchorService

            )
        {
            _logger = logger;

            _rawInputStore = rawInputStore;
            _store = store;
            _metadataStore = metadataStore;


            
            _inputProcessingOrchestrator = inputProcessingOrchestrator;
            
            _metadataFactory = metadataFactory;
            _classifier = classifier;
            _shotBuilder = shotBuilder;
            _meshBuilder = meshBuilder;
            
            _anchorService = anchorService;
        }

        public async Task<AttentionProcessResult> ProcessAsync(
            string userInput,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Processing attention request: {userInput}",
                userInput);

            var rawInput = new RawAttentionInput(
                Guid.NewGuid(),
                userInput,
                "POST /attention/input",
                DateTimeOffset.UtcNow);

            _rawInputStore.Save(rawInput);

            var inputContext = new InputProcessingContext(rawInput);


            await _inputProcessingOrchestrator.ProcessAsync(
                inputContext,
                cancellationToken
                );
                


            if (!inputContext.IsApprovedForEngine)
            {
                var invalidRawInput = rawInput with
                {
                    IsValid = false,
                    ValidationErrors = inputContext.ValidationResult.Errors
                };

                _rawInputStore.Update(invalidRawInput);

                _logger.LogWarning(
                    "RawInput {RawInputId} rejected before AttentionBall creation.",
                    rawInput.Id
                    );
                return new AttentionProcessResult(
                    false,
                    null,
                    new InvalidInputResponse(
                        rawInput.Id,
                        "Input validation failed.",
                        inputContext.ValidationResult.Errors));
                                          
            }

            var effectiveInput = inputContext.EffectiveText;

            var aspirations = _classifier.DetectAspirations(effectiveInput);

            var tendencies = _classifier.DetectTendencies(effectiveInput);

            var isAnchor = _anchorService.ShouldCreateAnchor(effectiveInput);

            var keywords =
                inputContext.KeywordExtractionResult?.Keywords.ToList()
                ?? new List<string>();

            var attentionBall = new AttentionBall(
                Guid.NewGuid(),
                rawInput.Id,
                effectiveInput,
                keywords,
                "project name if any",
                "Context must earn Persistence.",
                "Take the next meaningful action",
                10,
                1.0,
                0,
                isAnchor,
                DateTimeOffset.UtcNow,
                DateTimeOffset.UtcNow);

            _logger.LogInformation(
                "AttentionBall created: {Id}", attentionBall.Id
                );

            _store.Save( attentionBall);

            var metadata = _metadataFactory.Create(
                attentionBall,
                inputContext
                );

            _metadataStore .Save( metadata );

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

            return new AttentionProcessResult(
                true,
                new AttentionResponse(
                    attentionBall.CurrentAim,
                    attentionBall.ActiveProject,
                    attentionBall.MustNotForget,
                    attentionBall.NextMove,
                    attentionBall.Keywords,
                    aspirations.Select(x => x.Name).ToList(),
                    tendencies.Select(x => x.Name).ToList(),
                    relatedContext,
                    shot.Text),
                null);
               
        }
    }
}