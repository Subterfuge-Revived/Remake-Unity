using System;
using System.Diagnostics;
using SubterfugeRemakeService;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Rooms.Multiplayer.Game.Chat
{
    public class ChatController : MonoBehaviour
    {
        public ChatGroup scrollItemTemplate;
        public GameObject scrollContentContainer;
        
        private SubterfugeClient.SubterfugeClient client = ApplicationState.Client.getClient();

        private MessageGroup viewChatGroup = null;
        private GetMessageGroupsResponse groups = null;

        void Start()
        {
            if (ApplicationState.isMultiplayer)
            {
                // Load chat messages.
                groups = client.GetMessageGroups(
                    new GetMessageGroupsRequest()
                    {
                        RoomId = ApplicationState.CurrentGame.Configuration.Id,
                    });
            }
            else
            {
                var group = new MessageGroup()
                {
                    GroupId = "myGroup",
                };
                group.Messages.Add(new MessageModel()
                {
                    GroupId = "myGroup",
                    Id = "123123",
                    Message = "SomeMessage",
                    RoomId = "RoomId",
                    SenderId = "123123",
                    UnixTimeCreatedAt = DateTime.UtcNow.ToFileTimeUtc(),
                });
                group.GroupMembers.Add(new User()
                {
                    Id = "UserId",
                    Username = "User Two",
                });
                group.GroupMembers.Add(new User()
                {
                    Id = "UserId",
                    Username = "User Three",
                });
                
                groups = new GetMessageGroupsResponse()
                {
                    Status = new ResponseStatus()
                    {
                        IsSuccess = true,
                        Detail = "",
                    }
                };
                groups.MessageGroups.Add(group);
            }

            if (viewChatGroup == null)
            {
                loadGroupOverview();
            }
            else
            {
                // Set content to a list of groups.
            }
        }

        private void clearScrollList()
        {
            // Destroy all existing items.
            ChatGroup[] existingButtons = FindObjectsOfType<ChatGroup>();
            foreach (ChatGroup chatGroup in existingButtons)
            {
                if (chatGroup.isActiveAndEnabled)
                {
                    Destroy(chatGroup.gameObject);
                }
            }
        }

        private void instantiateChatGroupButton(MessageGroup messageGroup)
        {
            // Create a new templated item
            ChatGroup chatGroup = Instantiate(scrollItemTemplate, scrollContentContainer.transform);
            chatGroup.gameObject.SetActive(true);
            chatGroup.setMessageGroup(messageGroup);
            chatGroup.ChatController = this;
        }

        private void loadGroupOverview()
        {
            clearScrollList();
            if (groups.Status.IsSuccess)
            {
                foreach (MessageGroup msgGroup in groups.MessageGroups)
                {
                    instantiateChatGroupButton(msgGroup);
                }
            }
        }

        private void loadGroupMessages()
        {
            clearScrollList();
            if (groups.Status.IsSuccess)
            {
                foreach (MessageGroup msgGroup in groups.MessageGroups)
                {
                    instantiateChatGroupButton(msgGroup);
                }
            }
        }

        public void showChatOverview()
        {
            loadGroupOverview();
            viewChatGroup = null;
        }

        public void showGroupChat(MessageGroup messageGroup)
        {
            loadGroupMessages();
            viewChatGroup = messageGroup;
        }
    }
}