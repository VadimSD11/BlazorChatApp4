namespace BlazorChatApp4.Server.Data
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Sentiment { get; set; } 
        public DateTime Timestamp { get; set; }
    }

}
