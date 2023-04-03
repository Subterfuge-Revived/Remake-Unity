using Subterfuge.Remake.Api.Network;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoomButton : MonoBehaviour
{
    private GameConfiguration room;

    public TextMeshProUGUI gameTitle;
    public TextMeshProUGUI creatorName;
    public GameObject rankedIcon;
    public GameObject dominationIcon;
    public GameObject miningIcon;
    public TextMeshProUGUI players;
    public GameObject anonIcon;

    public void setGameRoom(GameConfiguration room)
    {
        this.room = room;
        if (room != null)
        {
            creatorName.text = "By: " + room.Creator.Username;
            gameTitle.text = room.RoomName;
            rankedIcon.SetActive(room.GameSettings.IsRanked);
            if (room.GameSettings.Goal == Goal.Domination)
            {
                miningIcon.SetActive(false);
            }
            else
            {
                dominationIcon.SetActive(false);
            }
            players.text = room.PlayersInLobby.Count + "/" + room.GameSettings.MaxPlayers;
            anonIcon.SetActive(room.GameSettings.IsAnonymous);
        }
    }

    // Update is called once per frame
    public void onClick()
    {
        // Set the gameroom to the selected game
        ApplicationState.SetActiveRoom(room);

        if (room.GameSettings.MaxPlayers == room.PlayersInLobby.Count || room.RoomStatus == RoomStatus.Ongoing)
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
