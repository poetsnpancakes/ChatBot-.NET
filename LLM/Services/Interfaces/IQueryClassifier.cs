namespace ChatBot.LLM.Services.Interfaces
{
    public interface IQueryClassifier
    {
        public Task<string> ClassifyQuery(string query);
    }
}
