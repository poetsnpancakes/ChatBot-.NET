using ChatBot.LLM.Services.Interfaces;

namespace ChatBot.LLM.Factory
{
    public interface IOpenAiFactory
    {
        IOpenAIService GetService(OpenAiModel model);
    }

}
