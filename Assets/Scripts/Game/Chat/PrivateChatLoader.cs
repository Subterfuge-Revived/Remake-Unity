using System;
using SubterfugeRemakeService;
using TMPro;
using UnityEditor;
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
        
        private void instantiateChatMessage(MessageModel message)
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
                foreach (MessageModel message in MessageGroup.Messages)
                {
                    instantiateChatMessage(message);
                }
            }
        }

        public void sendChatMessage(Text message)
        {
            var model = new MessageModel()
            {
                GroupId = MessageGroup.GroupId,
                Id = Guid.NewGuid().ToString(),
                Message = message.text,
                RoomId = ApplicationState.CurrentGame.Configuration.Id,
                SenderId = ApplicationState.player.GetId(),
                UnixTimeCreatedAt = DateTime.UtcNow.ToFileTimeUtc(),
            };
            
            // TODO: Create a 'Pending' message until we recieve a confirmation from the server.
            instantiateChatMessage(model);

            ApplicationState.Client.getClient().SendMessage(new SendMessageRequest()
            {
                GroupId = MessageGroup.GroupId,
                Message = message.text,
                RoomId = ApplicationState.CurrentGame.Configuration.Id,
            });
            
            message.text = null;
        }
    }
}