//gs
using Day13.LocalAiPingAPI.Interfaces;
using Day13.LocalAiPingAPI.Models;
using Day13.LocalAiPingAPI.Options;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OllamaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Day13.LocalAiPingAPI.Services
{
    //This class only handles local ai response generation.
    //endpoint touches the interface ie, IAiService
    //this class depends on IChatClient abstraction.

    //IChatClient 
    //Microsoft.Extensions.AI gives one common abstraction 
    // for chat models.

    public class LocalAiService : IAiService
    {
        private readonly AiOptions _options;

        private readonly IChatClient _chatClient;

        public LocalAiService(IOptions<AiOptions> options)
        {
            _options = options.Value;

            //ollamaApiClient implements IChatClient.
            //This connects the .NET app to local ollama Runtime

            _chatClient = new OllamaApiClient(
                new Uri(_options.BaseUrl),
                _options.Model

             );
        }

        public async Task<AiResponseDto> GenerateAsync(AiRequest request)
        {
            //defensive validation
            if (string.IsNullOrWhiteSpace(request.Prompt))
            {
                return new AiResponseDto
                {
                    Response = "Prompt cannot be empty",
                    Model = _options.Model
                };
            }

            //global system prompt comes from appsettings.json
            var globalSystemPrompt = _options.SystemPrompt;


            //optional per request instruction comes from user/API request
            var requestInstruction = request.SystemInstruction;

            var messages = new List<ChatMessage>();

            if (!string.IsNullOrWhiteSpace(globalSystemPrompt))
            {
                messages.Add(new ChatMessage(ChatRole.System, globalSystemPrompt));
            }

            if (!string.IsNullOrWhiteSpace(requestInstruction))
            {
                messages.Add(new ChatMessage(ChatRole.System, requestInstruction));
            }

            messages.Add(new ChatMessage(ChatRole.User, request.Prompt));

            //send messages through local IChatClient abstraction
            var response = await _chatClient.GetResponseAsync(messages);

            return new AiResponseDto
            {
                Response = response.Text,
                Model = _options.Model
            };

            //microsoft current IChatClient abstraction is designed for provider agnostic chat, and ollama sharp can  provide an  Ollama - Backed implementation for local models..

        }

        //streaming ai response
        public async IAsyncEnumerable<string> StreamAsync(AiRequest request)
        {
            //global system prompt
            var globalSystemPrompt = _options.SystemPrompt;

            //Optional request level system instruction
            var requestInstruction = request.SystemInstruction;

            var messages = new List<ChatMessage>();

            //global instruction

            if (! string.IsNullOrWhiteSpace(globalSystemPrompt))
            {
                messages.Add(
                    new ChatMessage(ChatRole.System, globalSystemPrompt)
                );
            }

            //request system instruction
            if (string.IsNullOrWhiteSpace(requestInstruction))
            {
                messages.Add(
                    new ChatMessage(ChatRole.System, requestInstruction)
                );
            }

            //user prompt
            messages.Add(
                new ChatMessage(ChatRole.User, request.Prompt)
            );

            //stream chunks from ollama
            await foreach (var update in _chatClient.GetStreamingResponseAsync(messages))
            {
                if(!string.IsNullOrWhiteSpace(update.Text))
                {
                    yield return update.Text; 
                }
            }
        }
    }
}