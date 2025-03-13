using System;

namespace ChatSystem.Core
{
    public class Message
    {
        public string Content { get; private set; }
        public string SenderName { get; private set; }
        public DateTime Timestamp { get; private set; }
        public MessageType Type { get; private set; }

        public Message(string content, string senderName, MessageType type = MessageType.Normal)
        {
            Content = content;
            SenderName = senderName;
            Timestamp = DateTime.Now;
            Type = type;
        }
    }

    public enum MessageType
    {
        Normal,
        System,
        Error,
        Important,
    }
}
