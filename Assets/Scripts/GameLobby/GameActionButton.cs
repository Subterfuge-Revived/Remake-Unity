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
        if (room.creator_id == ApplicationState.player.getId())
        {
            // Determine if there are more players than just the creator.
            if (room.players.Count > 1)
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
            foreach(NetworkUser player in room.players)
            {
                if (!isInGame && player.id == ApplicationState.player.getId())
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
        Api api = GetComponent<Api>();
        JoinLobbyResponse joinResponse = await api.JoinLobby(ApplicationState.currentGameRoom.room_id);
        NetworkUser user = new NetworkUser();
        user.id = ApplicationState.player.getId();
        user.name = ApplicationState.player.getPlayerName();
        
        ApplicationState.currentGameRoom.players.Add(user);
        
        // Reload the scene to update lobby.
        SceneManager.LoadScene("GameLobby");
    }
    
    public async void onStartEarly()
    {
        Api api = GetComponent<Api>();
        StartLobbyEarlyResponse startEarlyResponse = await api.StartLobbyEarly(ApplicationState.currentGameRoom.room_id);

        if (startEarlyResponse.success == true)
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
