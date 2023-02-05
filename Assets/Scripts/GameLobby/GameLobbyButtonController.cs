﻿using System.Threading.Tasks;
using SubterfugeCore.Models.GameEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rooms.Multiplayer.GameLobby
{
    public class GameLobbyButtonController : MonoBehaviour
    {
        private GameConfiguration currentGameConfig;
        
        public GameObject joinButton;
        public GameObject leaveButton;
        public GameObject deleteLobbyButton;
        public GameObject startEarlyButton;
        
        public void Start()
        {
            currentGameConfig = ApplicationState.currentGameConfig;
            updateButtonControls();
        }

        public async Task onJoin() {
            if (isPlayerInGame())
            {
                updateButtonControls();
            }
            else
            {
                var client = ApplicationState.Client.getClient();
                var joinResponse = await client.LobbyClient.JoinRoom(new JoinRoomRequest(), ApplicationState.currentGameConfig.Id);

                if (joinResponse.Status.IsSuccess)
                {
                    User user = new User();
                    user.Id = ApplicationState.player.GetId();
                    user.Username = ApplicationState.player.GetPlayerName();

                    ApplicationState.currentGameConfig.PlayersInLobby.Add(user);

                    if (ApplicationState.currentGameConfig.PlayersInLobby.Count ==
                        ApplicationState.currentGameConfig.GameSettings.MaxPlayers)
                    {
                        SceneManager.LoadScene("Game");
                    }

                    // Reload the scene to update lobby.
                    SceneManager.LoadScene("GameLobby");
                }
                else
                {
                    Debug.Log(joinResponse);
                    Debug.Log("Unable to join game");
                    // TODO: Add some text to notify the user they are offline.
                    // Potentially add the user's request to a queue that gets attempted when they regain connectivity.
                }
            }
        }
        
        public async Task onLeave() {
            if (isPlayerInGame())
            {
                var client = ApplicationState.Client.getClient();
                var leaveResponse = await client.LobbyClient.LeaveRoom(ApplicationState.currentGameConfig.Id);

                if (leaveResponse.Status.IsSuccess)
                {
                    // Reload the scene to update lobby.
                    ApplicationState.currentGameConfig = null;
                    SceneManager.LoadScene("GameSelect");
                }
                else
                {
                    // TODO: Add some text to tell the user they are offline.
                } 
            }
            else
            {
                updateButtonControls();
            }
        }
        
        public async Task onStartEarly() {
            if (didPlayerCreateGame())
            {
                var client = ApplicationState.Client.getClient();
                var startEarlyResponse = await client.LobbyClient.StartGameEarly(ApplicationState.currentGameConfig.Id);

                if (startEarlyResponse.Status.IsSuccess)
                {
                    // Load the config.
                    SceneManager.LoadScene("Game");
                }
                else
                {
                    // TODO: Tell the user that they are offline or send the error message.
                    // If offline, potentailly add their request to a queue.

                    // Reload the scene to update lobby. Handle error here.
                    SceneManager.LoadScene("GameLobby");
                }
            }
            else
            {
                updateButtonControls();
            }
        }
        
        public void onDelete() {
            onLeave();
        }

        private void updateButtonControls()
        {
            if (didPlayerCreateGame())
            {
                showCreatorControls();
            } else if (isPlayerInGame())
            {
                showPlayerLeaveButtons();
            }
            else
            {
                showPlayerJoinButtons();
            }
        }
        
        private bool isPlayerInGame()
        {
            foreach(User player in ApplicationState.currentGameConfig.PlayersInLobby)
            {
                if (player.Id == ApplicationState.player.GetId())
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool didPlayerCreateGame()
        {
            return ApplicationState.currentGameConfig.Creator.Id == ApplicationState.player.GetId();
        }

        private void showPlayerJoinButtons()
        {
            leaveButton.SetActive(false);
            joinButton.SetActive(true);
            deleteLobbyButton.SetActive(false);
            startEarlyButton.SetActive(false);
        }

        private void showPlayerLeaveButtons()
        {
            leaveButton.SetActive(true);
            joinButton.SetActive(false);
            deleteLobbyButton.SetActive(false);
            startEarlyButton.SetActive(false);
        }
        
        private void showCreatorControls()
        {
            leaveButton.SetActive(false);
            joinButton.SetActive(false);
            deleteLobbyButton.SetActive(true);
            if (ApplicationState.currentGameConfig.PlayersInLobby.Count > 1)
            {
                startEarlyButton.SetActive(true);
            }
            else
            {
                startEarlyButton.SetActive(false);
            }
        }
    }
}