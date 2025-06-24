using System.Text;
using DesktopService.Services.contracts;
using Microsoft.Extensions.AI;

namespace DesktopService.Services.implementations;

public class DeepSeekTranslate : IDeepSeekTranslate
{
    private const string promptFirstPart = "Translate me the sentence between quotation marks";
    private const string promptSecondPart = "from English to the Polish language. The output should be the sentence in the Polish language, response should be as concise as it is possible, only the sentence, because I want to save it to the variable in my program. Do not add any additional comments to your response, only the translation";

    // Injected Reusable Services
    private readonly IChatClient _client;
    private readonly List<ChatMessage> _chatMessages;

    public DeepSeekTranslate(IChatClient client, List<ChatMessage> chatMessages)
    {
        _client = client;
        _chatMessages = chatMessages;
    }
    public async Task<string> TranslateAnkiSentence(string sentenceToTranslate)
    {
        string response = await GetTranslatedResponseAsync(sentenceToTranslate);
        return response;
    }

    private async Task<string> GetTranslatedResponseAsync(string sentenceToTranslate)
    {
        string message = $"{promptFirstPart} '{sentenceToTranslate}' {promptSecondPart}";
        _chatMessages.Add(new ChatMessage(ChatRole.User, message));
        var response = new StringBuilder();
        await foreach (ChatResponseUpdate item in _client.GetStreamingResponseAsync(_chatMessages))
        {
            response.Append(item.Text);
        }
        _chatMessages.Add(new ChatMessage(ChatRole.Assistant, response.ToString()));
        return response.ToString();
    }

}
