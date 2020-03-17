using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoomButton : MonoBehaviour
{
    public GameRoom room;

    // Update is called once per frame
    public void onClick()
    {
        // Set the gameroom to the selected game
        ApplicationState.currentGameRoom = room;
        
        // Load the game scene
        SceneManager.LoadScene("GameLobby");
    }
}
