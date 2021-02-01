using SubterfugeRemakeService;
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
        Room room = ApplicationState.currentGameRoom;
        lobbyDetails.text = "Room Id: " + room.RoomId + "\nTitle: " + room.RoomName + "\nAnonymous: " + room.Anonymous + "\nCreator: " + room.Creator.Username + "\nGoal: " + room.Goal + "\nRanked: " + room.RankedInformation.IsRanked + "\nMinimum Rank: " + room.RankedInformation.MinRating + "\nPlayers (" + room.Players.Count + "):";
        foreach (User user in room.Players)
        {
            lobbyDetails.text = lobbyDetails.text + "\n" + user.Username;
        }
    }
}
