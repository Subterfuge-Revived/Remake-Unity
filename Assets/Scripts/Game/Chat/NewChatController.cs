using System.Collections.Generic;
using System.Linq;
using SubterfugeCore.Models.GameEvents;
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
            
            var groupResponse = await ApplicationState.Client.getClient().GroupClient.CreateMessageGroup(messageGroupRequest, ApplicationState.currentGameConfig.Id);
            if (!groupResponse.Status.IsSuccess)
            {
                // TODO: !
            }

            // Additionally, send the message to the group
            var response = await ApplicationState.Client.getClient().GroupClient.SendMessage(new SendMessageRequest()
            {
                Message = message.text,
            }, ApplicationState.currentGameConfig.Id, groupResponse.GroupId);
            
            message.text = null;
            
            var reloadGroupMessages = await ApplicationState.Client.getClient().GroupClient.GetMessageGroups(ApplicationState.currentGameConfig.Id);
            ChatController.showPrivateChat(reloadGroupMessages.MessageGroups.First(group => group.Id == groupResponse.GroupId));
        }

        
    }
}