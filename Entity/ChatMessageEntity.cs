namespace ChatBot.Entity
{
    public class ChatMessageEntity
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
       

        public string Role { get; set; } // "user" or "assistant"
        public string Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
