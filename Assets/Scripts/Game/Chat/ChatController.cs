using System;
using System.Diagnostics;
using SubterfugeRemakeService;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Rooms.Multiplayer.Game.Chat
{
    public class ChatController : MonoBehaviour
    {
        public GameObject ChatOverviewCanvas;
        public GameObject PrivateChatViewCanvas;

        private void Start()
        {
            showChatOverview();
        }

        public void showChatOverview()
        {
            disableAll();
            ChatOverviewCanvas.SetActive(true);
        }

        public void showPrivateChat(MessageGroup messageGroup)
        {
            disableAll();
            PrivateChatViewCanvas.SetActive(true);
            ((PrivateChatLoader)PrivateChatViewCanvas.GetComponent<PrivateChatLoader>()).loadChatMessages(messageGroup);
        }

        public void disableAll()
        {
            ChatOverviewCanvas.SetActive(false);
            PrivateChatViewCanvas.SetActive(false);
        }
    }
}