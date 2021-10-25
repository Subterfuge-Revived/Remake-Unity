using System;
using SubterfugeRemakeService;
using UnityEngine;

namespace Rooms.Multiplayer.Game.Chat
{
    public class ChatOverviewLoader : MonoBehaviour
    {
        public ChatController ChatController;
        public ChatGroupListItem scrollItemTemplate;
        public GameObject scrollContentContainer;
        
        private SubterfugeClient.SubterfugeClient client = ApplicationState.Client.getClient();
        
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
                createSampleGroups();
            }
            loadGroupOverview();
        }

        private void createSampleGroups()
        {
            var group = new MessageGroup()
            {
                GroupId = "myGroup",
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
        
        private MessageModel createSampleMessage(string userId, string message)
        {
            return new MessageModel()
            {
                GroupId = "myGroup",
                Id = "123123",
                Message = message,
                RoomId = "RoomId",
                SenderId = userId,
                UnixTimeCreatedAt = DateTime.UtcNow.ToFileTimeUtc(),
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
            if (groups.Status.IsSuccess)
            {
                foreach (MessageGroup msgGroup in groups.MessageGroups)
                {
                    instantiateChatGroupButton(msgGroup);
                }
            }
        }
    }
}