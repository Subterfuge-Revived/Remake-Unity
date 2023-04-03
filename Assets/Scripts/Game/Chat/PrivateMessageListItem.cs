using System.Globalization;
using Subterfuge.Remake.Api.Network;
using TMPro;
using UnityEngine;

namespace Rooms.Multiplayer.Game.Chat
{
    public class PrivateMessageListItem : MonoBehaviour
    {
        private ChatMessage _message;
        public TextMeshProUGUI SenderInfo;
        public TextMeshProUGUI messageContent;

        public void setMessage(ChatMessage _message)
        {
            SenderInfo.text = _message.SentBy.Username + "   at " + _message.SentAt.ToString(CultureInfo.CurrentCulture);
            messageContent.text = _message.Message;
        }
    }
}