using System;
using Subterfuge.Remake.Api.Client;
using Subterfuge.Remake.Api.Network;
using UnityEngine;

namespace Rooms.Multiplayer.Game.Chat
{
    public class ChatOverviewLoader : MonoBehaviour
    {
        public ChatController ChatController;
        public ChatGroupListItem scrollItemTemplate;
        public GameObject scrollContentContainer;
        
        private SubterfugeClient client = ApplicationState.Client.getClient();
        
        private GetMessageGroupsResponse groups = null;
        
        async void Start()
        {
            if (ApplicationState.isMultiplayer)
            {
                // Load chat messages.
                var response = await client.GroupClient.GetMessageGroups(ApplicationState.currentGameConfig.Id);
                response.Get(
                    (succeess) =>
                    {
                        groups = succeess;
                    },
                    (failure) =>
                    {
                        groups = null;
                    }
                );
            }
            else
            {
                createSampleGroups();
            }
            loadGroupOverview();
        }

        private void createSampleGroups()
        {
            var group = new MessageGroup()
            {
                Id = "myGroup",
            };
            group.Messages.Add(createSampleMessage("SomeUser", "Some Message!"));
            group.Messages.Add(createSampleMessage("SomeUserTwo", "Some Other Message!"));
            group.Messages.Add(createSampleMessage("SomeUserThree", "Another Message!"));
            group.Messages.Add(createSampleMessage("SomeUserFour", "Long Message \n Spanning multiple \n lines!!!"));
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
                
            groups = new GetMessageGroupsResponse();
            groups.MessageGroups.Add(group);
        }
        
        private ChatMessage createSampleMessage(string userId, string message)
        {
            return new ChatMessage()
            {
                GroupId = "myGroup",
                Id = "123123",
                Message = message,
                RoomId = "RoomId",
                
                SentBy = ApplicationState.player.PlayerInstance.ToUser().ToSimpleUser(),
                SentAt = DateTime.UtcNow,
            };
        }
        
        private void clearScrollList()
        {
            // Destroy all existing items.
            ChatGroupListItem[] existingButtons = FindObjectsOfType<ChatGroupListItem>();
            foreach (ChatGroupListItem chatGroup in existingButtons)
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
            ChatGroupListItem chatGroupListItem = Instantiate(scrollItemTemplate, scrollContentContainer.transform);
            chatGroupListItem.gameObject.SetActive(true);
            chatGroupListItem.setMessageGroup(messageGroup);
            chatGroupListItem.ChatController = ChatController;
        }

        private void loadGroupOverview()
        {
            clearScrollList();
            if (groups != null)
            {
                foreach (MessageGroup msgGroup in groups.MessageGroups)
                {
                    instantiateChatGroupButton(msgGroup);
                }
            }
        }
    }
}