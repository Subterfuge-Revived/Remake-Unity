using SubterfugeCore.Models.GameEvents;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Rooms.Multiplayer.Game.Chat
{
    public class ParticipantListing : MonoBehaviour
    {
        public NewChatController NewChatController;
        public TextMeshProUGUI participantName;
        public TextMeshProUGUI participantExpLevel;
        public GameObject selectedIcon;
        // TODO: ParticipantSymbol

        private User user;
        private bool isSelected = false;

        public void setUser(User participant)
        {
            user = participant;
            participantName.text = participant.Username;
            participantExpLevel.text = "Lv. " + new Random().Next(0, 100);
        }

        public void OnSelect()
        {
            isSelected = !isSelected;
            selectedIcon.SetActive(isSelected);
            if (isSelected)
            {
                NewChatController.selectParticipant(user);
            }
            else
            {
                NewChatController.deselectParticipant(user);
            }
        }
    }
}