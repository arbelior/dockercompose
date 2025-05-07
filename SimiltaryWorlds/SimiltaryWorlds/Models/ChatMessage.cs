namespace SimiltaryWorlds.Models
{
    public class ChatMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }

        public static ChatMessage FromUser(string content) => new() { Role = "user", Content = content };
        public static ChatMessage FromSystem(string content) => new() { Role = "system", Content = content };
        public static ChatMessage FromAssistant(string content) => new() { Role = "assistant", Content = content };
    }
}
