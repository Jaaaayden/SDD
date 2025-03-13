using System;
using System.Collections.Generic;
using ChatSystem.Utils;

namespace ChatSystem.Core
{
    public class ChatBox
    {
        private readonly int maxMessages;
        private readonly Queue<Message> messageHistory;
        private readonly DebugLogger logger;

        public event Action<Message> OnMessageReceived;

        public ChatBox(int maxStoredMessages = 100)
        {
            maxMessages = maxStoredMessages;
            messageHistory = new Queue<Message>(maxMessages);
            logger = new DebugLogger();
        }

        public void AddMessage(string content, string sender, MessageType type = MessageType.Normal)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new ArgumentException("Message content cannot be empty");
                }

                var message = new Message(content, sender, type);

                if (messageHistory.Count >= maxMessages)
                {
                    messageHistory.Dequeue();
                }

                messageHistory.Enqueue(message);
                OnMessageReceived?.Invoke(message);

                logger.Log($"Message added: {sender}: {content}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error adding message: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Message> GetMessageHistory()
        {
            return messageHistory.ToArray();
        }

        public void ClearHistory()
        {
            messageHistory.Clear();
            logger.Log("Chat history cleared");
        }
    }
}
