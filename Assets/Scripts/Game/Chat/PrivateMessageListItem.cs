using System;
using System.Globalization;
using System.Linq;
using SubterfugeRemakeService;
using TMPro;
using UnityEngine;

namespace Rooms.Multiplayer.Game.Chat
{
    public class PrivateMessageListItem : MonoBehaviour
    {
        private MessageModel _message;
        public TextMeshProUGUI SenderInfo;
        public TextMeshProUGUI messageContent;

        public void setMessage(MessageModel _message)
        {
            SenderInfo.text = _message.SenderId + "   at " + DateTime.FromFileTimeUtc(_message.UnixTimeCreatedAt).ToString(CultureInfo.CurrentCulture);
            messageContent.text = _message.Message;
        }
    }
}