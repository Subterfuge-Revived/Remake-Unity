using System;
using Subterfuge.Remake.Api.Network;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.Game.Chat
{
    public class PrivateChatLoader : MonoBehaviour
    {
        public ChatController ChatController;
        public PrivateMessageListItem scrollItemTemplate;
        public GameObject scrollContentContainer;
        public MessageGroup MessageGroup;

        private void clearScrollList()
        {
            // Destroy all existing items.
            PrivateMessageListItem[] existingButtons = FindObjectsOfType<PrivateMessageListItem>();
            foreach (PrivateMessageListItem chatGroup in existingButtons)
            {
                if (chatGroup.isActiveAndEnabled)
                {
                    Destroy(chatGroup.gameObject);
                }
            }
        }
        
        private void instantiateChatMessage(ChatMessage message)
        {
            // Create a new templated item
            PrivateMessageListItem chatGroupListItem = Instantiate(scrollItemTemplate, scrollContentContainer.transform);
            chatGroupListItem.gameObject.SetActive(true);
            chatGroupListItem.setMessage(message);
        }

        public void loadChatMessages(MessageGroup messageGroup)
        {
            MessageGroup = messageGroup;
            clearScrollList();
            if (MessageGroup != null)
            {
                foreach (ChatMessage message in MessageGroup.Messages)
                {
                    instantiateChatMessage(message);
                }
            }
        }

        public void sendChatMessage(Text message)
        {
            // TODO: Create a 'Pending' message until we recieve a confirmation from the server.
            instantiateChatMessage(new ChatMessage()
            {
                Message = message.text,
                SentAt = DateTime.UtcNow,
                SentBy = ApplicationState.player.PlayerInstance.ToUser().ToSimpleUser()
            });

            ApplicationState.Client.getClient().GroupClient.SendMessage(new SendMessageRequest()
            {
                Message = message.text,
            }, ApplicationState.currentGameConfig.Id, MessageGroup.Id);
            
            message.text = null;
        }
    }
}