using System.Collections;
using System.Collections.Generic;
using SubterfugeRemakeService;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoomButton : MonoBehaviour
{
    public GameConfiguration room;

    public TextMeshProUGUI gameTitle;
    public TextMeshProUGUI minRating;
    public TextMeshProUGUI goal;
    public TextMeshProUGUI players;
    public TextMeshProUGUI anon;

    public void Start()
    {
        loadStrings();
    }

    public void loadStrings()
    {
        if (room != null)
        {
            gameTitle.text = room.RoomName;
            minRating.text = room.GameSettings.IsRanked.ToString();
            goal.text = room.GameSettings.Goal.ToString();
            players.text = room.Players.Count + "/" + room.GameSettings.MaxPlayers;
            minRating.text = room.GameSettings.Anonymous ? "Anon" : "";
        }
    }

    // Update is called once per frame
    public void onClick()
    {
        // Set the gameroom to the selected game
        ApplicationState.SetActiveRoom(room);

        if (room.GameSettings.MaxPlayers == room.Players.Count || room.RoomStatus == RoomStatus.Ongoing)
        {
            // Load the game scene
            SceneManager.LoadScene("Game");   
        }
        else
        {
            // Load the game scene
            SceneManager.LoadScene("GameLobby");
        }
    }
    
}
