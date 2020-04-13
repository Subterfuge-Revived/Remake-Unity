using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameActionButton : MonoBehaviour
{
    public Button actionButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // Determine if the current user is the creator of the game
        GameRoom room = ApplicationState.currentGameRoom;
        if (room.CreatorId == ApplicationState.player.GetId())
        {
            // Determine if there are more players than just the creator.
            if (room.Players.Count > 1)
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = "Start Game Early";
                actionButton.onClick.AddListener(onStartEarly);
            }
            else
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = "Waiting for players";
                actionButton.interactable = false;
            }
        }
        else
        {
            bool isInGame = false;
            foreach(NetworkUser player in room.Players)
            {
                if (!isInGame && player.Id == ApplicationState.player.GetId())
                {
                    isInGame = true;
                }
            }

            if (isInGame)
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = "Waiting for players";
                actionButton.interactable = false;                
            }
            else
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = "Join Game";
                actionButton.onClick.AddListener(onJoinLobby);
            }
        }
    }

    public async void onJoinLobby()
    {
        Api api = new Api();
        JoinLobbyResponse joinResponse = await api.JoinLobby(ApplicationState.currentGameRoom.RoomId);
        NetworkUser user = new NetworkUser();
        user.Id = ApplicationState.player.GetId();
        user.Name = ApplicationState.player.GetPlayerName();
        
        ApplicationState.currentGameRoom.Players.Add(user);
        
        // Reload the scene to update lobby.
        SceneManager.LoadScene("GameLobby");
    }
    
    public async void onStartEarly()
    {
        Api api = new Api();
        StartLobbyEarlyResponse startEarlyResponse = await api.StartLobbyEarly(ApplicationState.currentGameRoom.RoomId);

        if (startEarlyResponse.Success == true)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            // Reload the scene to update lobby. Handle error here.
            SceneManager.LoadScene("GameLobby");
        }
    }

    public async void onCancel()
    {
        ApplicationState.currentGameRoom = null;
        SceneManager.LoadScene("GameSelect");
    }
}
