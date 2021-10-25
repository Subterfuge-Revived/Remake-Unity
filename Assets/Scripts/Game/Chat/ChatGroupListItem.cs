using System;
using System.Linq;
using SubterfugeRemakeService;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rooms.Multiplayer.Game.Chat
{
    public class ChatGroupListItem : MonoBehaviour
    {
        private MessageGroup _messageGroup;
        public TextMeshProUGUI memberList;
        public TextMeshProUGUI messageList;
        public ChatController ChatController;

        public void setMessageGroup(MessageGroup messageGroup)
        {
            _messageGroup = messageGroup;
            memberList.text =
                string.Join(",", messageGroup.GroupMembers.ToList().ConvertAll(member => member.Username));
            messageList.text = string.Join(",", messageGroup.Messages.ToList().ConvertAll(message => message.Message.ToString()));
        }

        public void onClick()
        {
            ChatController.showPrivateChat(_messageGroup);
        }
    }
}