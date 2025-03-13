using System;
using System.Collections.Generic;
using ChatSystem.Core;
using ChatSystem.Utils;

namespace ChatSystem.Managers
{
    public class DialogueManager
    {
        private readonly ChatBox chatBox;
        private readonly DebugLogger logger;
        private readonly Dictionary<string, Action> dialogueEvents;
        private bool isDialogueActive;

        public DialogueManager(ChatBox chatBox)
        {
            this.chatBox = chatBox;
            logger = new DebugLogger();
            dialogueEvents = new Dictionary<string, Action>();
            isDialogueActive = false;
        }

        public void StartDialogue(string npcName, string[] dialogueLines)
        {
            try
            {
                if (isDialogueActive)
                {
                    logger.LogWarning("Attempting to start dialogue while another is active");
                    return;
                }

                isDialogueActive = true;

                foreach (string line in dialogueLines)
                {
                    chatBox.AddMessage(line, npcName, MessageType.Important);
                }

                isDialogueActive = false;
                logger.Log($"Dialogue completed for NPC: {npcName}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in dialogue system: {ex.Message}");
                isDialogueActive = false;
                throw;
            }
        }

        public void RegisterDialogueEvent(string eventKey, Action callback)
        {
            if (!dialogueEvents.ContainsKey(eventKey))
            {
                dialogueEvents.Add(eventKey, callback);
                logger.Log($"Registered dialogue event: {eventKey}");
            }
        }

        public void TriggerDialogueEvent(string eventKey)
        {
            if (dialogueEvents.TryGetValue(eventKey, out Action callback))
            {
                callback?.Invoke();
                logger.Log($"Triggered dialogue event: {eventKey}");
            }
            else
            {
                logger.LogWarning($"Dialogue event not found: {eventKey}");
            }
        }

        public void ClearDialogueEvents()
        {
            dialogueEvents.Clear();
            logger.Log("Cleared all dialogue events");
        }
    }
}
