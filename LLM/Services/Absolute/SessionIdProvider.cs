namespace ChatBot.LLM.Services.Absolute
{
    public class SessionIdProvider
    {
        public string SessionId { get; private set; } = Guid.NewGuid().ToString();

        public void OverrideSessionId(string id)
        {
            SessionId = id;
        }
    }

}
