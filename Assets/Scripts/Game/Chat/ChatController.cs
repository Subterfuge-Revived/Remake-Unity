﻿using Subterfuge.Remake.Api.Network;
using UnityEngine;

namespace Rooms.Multiplayer.Game.Chat
{
    public class ChatController : MonoBehaviour
    {
        public GameObject ChatOverviewCanvas;
        public GameObject PrivateChatViewCanvas;
        public GameObject NewChatViewCanvas;

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

        public void showCreateNewChat()
        {
            disableAll();
            NewChatViewCanvas.SetActive(true);
            ((NewChatController)NewChatViewCanvas.GetComponent<NewChatController>()).loadParticipantList();
        }

        public void disableAll()
        {
            ChatOverviewCanvas.SetActive(false);
            PrivateChatViewCanvas.SetActive(false);
            NewChatViewCanvas.SetActive(false);
        }
    }
}