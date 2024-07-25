namespace BlazorChatApp4.Server.Services
{
    using Azure;
    using Azure.AI.TextAnalytics;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    public class SentimentAnalysisService
    {
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public SentimentAnalysisService(IConfiguration configuration)
        {
            var endpoint = new Uri(configuration["TextAnalytics:Endpoint"]);
            var apiKey = new AzureKeyCredential(configuration["TextAnalytics:ApiKey"]);
            _textAnalyticsClient = new TextAnalyticsClient(endpoint, apiKey);
        }

        public async Task<string> AnalyzeSentimentAsync(string text)
        {
            var documentSentiment = await _textAnalyticsClient.AnalyzeSentimentAsync(text);
            return documentSentiment.Value.Sentiment.ToString();
        }
    }

}
