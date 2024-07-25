using BlazorChatApp4.Server.Data;
using Microsoft.Data.SqlClient;

namespace BlazorChatApp4.Server.Services
{
    public class ChatMessageService
    {
        private readonly string _connectionString;

        public ChatMessageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task SaveMessageAsync(ChatMessage message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO ChatMessages (Username, Message, Sentiment, Timestamp) VALUES (@Username, @Message, @Sentiment, @Timestamp)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", message.Username);
                    command.Parameters.AddWithValue("@Message", message.Message);
                    command.Parameters.AddWithValue("@Sentiment", message.Sentiment ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Timestamp", message.Timestamp);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<ChatMessage>> GetMessagesAsync()
        {
            var messages = new List<ChatMessage>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Username, Message, Sentiment, Timestamp FROM ChatMessages";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            messages.Add(new ChatMessage
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Message = reader.GetString(2),
                                Sentiment = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Timestamp = reader.GetDateTime(4)
                            });
                        }
                    }
                }
            }
            return messages;
        }
    }

}
