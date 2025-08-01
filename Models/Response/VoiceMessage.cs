namespace ChatBot.Models.Response
{
    public class VoiceMessage
    {
        public string Type { get; set; }
        public string VoicePrompt { get; set; }
        public bool Last { get; set; }
    }

}
