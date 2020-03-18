using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.UI;

public class ShowLobbyInfo : MonoBehaviour
{
    public Text lobbyDetails;
    
    // Start is called before the first frame update
    void Start()
    {
        this.displayLobbyInformation();
    }

    public void displayLobbyInformation()
    {
        GameRoom room = ApplicationState.currentGameRoom;
        lobbyDetails.text = "Room Id: " + room.room_id + "\nTitle: " + room.description + "\nMap: " + room.map + "\nAnonymous: " + room.anonimity + "\nCreator: " + room.creator_id + "\nGoal: " + room.goal + "\nRanked: " + room.rated + "\nMinimum Rank: " + room.min_rating + "\nPlayers (" + room.players.Count + "):";
        foreach (NetworkUser netUser in room.players)
        {
            lobbyDetails.text = lobbyDetails.text + "\n" + netUser.name;
        }
    }
}
