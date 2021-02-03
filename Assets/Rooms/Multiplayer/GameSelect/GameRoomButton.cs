using System.Collections;
using System.Collections.Generic;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoomButton : MonoBehaviour
{
    public Room room;

    // Update is called once per frame
    public void onClick()
    {
        // Set the gameroom to the selected game
        ApplicationState.SetActiveRoom(room);

        if (room.MaxPlayers == room.Players.Count || room.RoomStatus == RoomStatus.Ongoing)
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
