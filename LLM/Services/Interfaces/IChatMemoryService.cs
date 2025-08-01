using ChatBot.Entity;
using OpenAI.Chat;

namespace ChatBot.LLM.Services.Interfaces
{
    public interface IChatMemoryService
    {
        Task AddMessageAsync(string sessionId, string role, string content);
        Task<List<ChatMessage>> GetMessagesAsync(string sessionId);
        Task ClearMessagesAsync(string sessionId);
    }

}
