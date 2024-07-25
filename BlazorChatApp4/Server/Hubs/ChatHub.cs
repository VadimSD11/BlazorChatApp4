using BlazorChatApp4.Server.Data;
using BlazorChatApp4.Server.Services;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private readonly ChatMessageService _chatMessageService;
    private readonly SentimentAnalysisService _sentimentAnalysisService;

    public ChatHub(ChatMessageService chatMessageService, SentimentAnalysisService sentimentAnalysisService)
    {
        _chatMessageService = chatMessageService;
        _sentimentAnalysisService = sentimentAnalysisService;
    }

    public async Task SendMessage(string user, string message)
    {
        var sentiment = await _sentimentAnalysisService.AnalyzeSentimentAsync(message);
        var chatMessage = new ChatMessage
        {
            Username = user,
            Message = message,
            Sentiment = sentiment,
            Timestamp = DateTime.UtcNow
        };

        await _chatMessageService.SaveMessageAsync(chatMessage);
        await Clients.All.SendAsync("ReceiveMessage", user, message, sentiment);
    }
}
