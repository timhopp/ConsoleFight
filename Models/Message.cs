namespace Demo.Services
{
    public class Message
    {

        public string From { get; private set; }
        public string Content { get; private set; }
        public Message(string from, string content)
        {
            Content = content;
            From = from;
        }
    }
}