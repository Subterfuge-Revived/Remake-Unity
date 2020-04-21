using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveGameButton : MonoBehaviour
{
    public Button leaveButton;
    // Start is called before the first frame update
    void Start()
    {
        //Determine if the user in in the game.
        bool isInGame = false;
        foreach(NetworkUser player in ApplicationState.currentGameRoom.Players)
        {
            if (!isInGame && player.Id == ApplicationState.player.GetId())
            {
                isInGame = true;
            }
        }

        if (isInGame)
        {
            // Determine if the current user is the creator of the game
            GameRoom room = ApplicationState.currentGameRoom;
            if (room.CreatorId == ApplicationState.player.GetId())
            {
                Text buttonText = leaveButton.GetComponentInChildren<Text>();
                buttonText.text = "Delete Lobby";
            }
            else
            {
                Text buttonText = leaveButton.GetComponentInChildren<Text>();
                buttonText.text = "Leave Game";
            }
            leaveButton.onClick.AddListener(onLeaveLobby);            
        }
        else
        {
            leaveButton.gameObject.SetActive(false);
        }
    }

    public async void onLeaveLobby()
    {
        Api api = new Api();
        NetworkResponse<LeaveLobbyResponse> leaveResponse = await api.LeaveLobby(ApplicationState.currentGameRoom.RoomId);

        if (leaveResponse.IsSuccessStatusCode())
        {
            // Reload the scene to update lobby.
            ApplicationState.currentGameRoom = null;
            SceneManager.LoadScene("GameSelect");
        }
        else
        {
            // TODO: Add some text to tell the user they are offline.
        }
    }
}
