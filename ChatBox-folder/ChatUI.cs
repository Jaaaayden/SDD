using UnityEngine;
using UnityEngine.UI;
using ChatSystem.Core;
using ChatSystem.Utils;
using System;

namespace ChatSystem.UI
{
    public class ChatBoxUI : MonoBehaviour
    {
        [SerializeField] private RectTransform chatPanel;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private InputField inputField;
        [SerializeField] private Button sendButton;
        [SerializeField] private Text messageDisplayPrefab;
        [SerializeField] private float messageSpacing = 5f;

        private ChatBox chatBox;
        private TextFormatter textFormatter;
        private DebugLogger logger;

        private void Awake()
        {
            try
            {
                chatBox = new ChatBox();
                textFormatter = new TextFormatter();
                logger = new DebugLogger();

                chatBox.OnMessageReceived += DisplayMessage;

                if (sendButton != null)
                    sendButton.onClick.AddListener(SendMessage);

                if (inputField != null)
                    inputField.onEndEdit.AddListener(OnInputEndEdit);

                logger.Log("ChatBoxUI initialized successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error initializing ChatBoxUI: {ex.Message}");
            }
        }

        private void OnInputEndEdit(string value)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            try
            {
                if (string.IsNullOrEmpty(inputField.text)) return;

                string formattedText = textFormatter.FormatText(inputField.text);
                chatBox.AddMessage(formattedText, "Player");
                inputField.text = string.Empty;
                inputField.ActivateInputField();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error sending message: {ex.Message}");
            }
        }

        private void DisplayMessage(Message message)
        {
            try
            {
                Text messageText = Instantiate(messageDisplayPrefab, chatPanel);
                messageText.text = $"{message.SenderName}: {message.Content}";

                // Apply styling based on message type
                switch (message.Type)
                {
                    case MessageType.Important:
                        messageText.color = Color.yellow;
                        break;
                    case MessageType.Error:
                        messageText.color = Color.red;
                        break;
                    case MessageType.System:
                        messageText.color = Color.cyan;
                        break;
                }

                // Adjust content size and scroll to bottom
                LayoutRebuilder.ForceRebuildLayoutImmediate(chatPanel);
                Canvas.ForceUpdateCanvases();
                scrollRect.normalizedPosition = new Vector2(0, 0);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error displaying message: {ex.Message}");
            }
        }

        private void OnDestroy()
        {
            if (chatBox != null)
                chatBox.OnMessageReceived -= DisplayMessage;
        }
    }
}
