using System.Collections.Generic;
using System.Linq;
using Subterfuge.Remake.Api.Network;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer.Game.Chat
{
    public class NewChatController : MonoBehaviour
    {
        private List<User> selectedParticipants = new List<User>();

        private Dictionary<User, ParticipantListing> participants = new Dictionary<User, ParticipantListing>();
        
        public ParticipantListing scrollItemTemplate;
        public GameObject scrollContentContainer;
        public ChatController ChatController;
        
        
        public void loadParticipantList()
        {
            clearScrollList();
            var userlist = ApplicationState.currentGameConfig.PlayersInLobby?.ToList();
            if (userlist.Count == 0)
            {
                userlist = new List<User>();
                userlist.Add(new User() { Id = "1", Username = "R10t--"});
                userlist.Add(new User() { Id = "2", Username = "R10t222"});
            }

            foreach (User user in userlist)
            {
                ParticipantListing chatGroupListItem =
                    Instantiate(scrollItemTemplate, scrollContentContainer.transform);
                chatGroupListItem.gameObject.SetActive(true);
                chatGroupListItem.setUser(user);
                participants.Add(user, chatGroupListItem);
            }
        }
        
        private void clearScrollList()
        {
            // Destroy all existing items.
            ParticipantListing[] existingParticipantMembers = FindObjectsOfType<ParticipantListing>();
            foreach (ParticipantListing participantListing in existingParticipantMembers)
            {
                if (participantListing.isActiveAndEnabled)
                {
                    Destroy(participantListing.gameObject);
                }
            }
        }

        public void selectParticipant(User user)
        {
            if (participants.ContainsKey(user))
            {
                selectedParticipants.Add(user);
            }
        }
        
        public void deselectParticipant(User user)
        {
            if (participants.ContainsKey(user) && selectedParticipants.Contains(user))
            {
                selectedParticipants.Remove(user);
            }
        }

        public async void sendChatMessage(Text message)
        {
            var messageGroupRequest = new CreateMessageGroupRequest();
            messageGroupRequest.UserIdsInGroup.AddRange(selectedParticipants.ConvertAll(participant => participant.Id));
            
            var createGroupResponse = (await ApplicationState
                .Client
                .getClient()
                .GroupClient
                .CreateMessageGroup(messageGroupRequest, ApplicationState.currentGameConfig.Id))
                .Get(
                (success) =>
                {
                    return success;
                },
                (failure) =>
                {
                    // TODO: !
                    return null;
                }
            );

            if (createGroupResponse == null)
                return;
            
            
            // Additionally, send the message to the group
            var sendMessageResponse = await ApplicationState.Client.getClient().GroupClient.SendMessage(new SendMessageRequest()
            {
                Message = message.text,
            }, ApplicationState.currentGameConfig.Id, createGroupResponse.GroupId);

            sendMessageResponse.Get(
                async (success) =>
                {
                    message.text = null;
                    (await ApplicationState
                        .Client
                        .getClient()
                        .GroupClient
                        .GetMessageGroups(ApplicationState.currentGameConfig.Id))
                        .Get(
                            (messageGRoupResponse) =>
                            {
                                ChatController.showPrivateChat(messageGRoupResponse.MessageGroups.First(group => group.Id == createGroupResponse.GroupId));
                            },
                            (failure) =>
                            {
                                
                            }
                        );
                },
                (failure) =>
                {
                    
                }
            );
        }

        
    }
}