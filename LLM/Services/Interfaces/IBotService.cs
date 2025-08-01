using ChatBot.Models.Response;

namespace ChatBot.LLM.Services.Interfaces
{
    public interface IBotService
    {
        public Task<BotResponse> QueryBot(string query);

        public IAsyncEnumerable<string> ResponsiveQueryBot(string query, string sessionId);
    }
}
