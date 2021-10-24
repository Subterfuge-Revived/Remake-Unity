using System;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.UI;

public class ShowLobbyInfo : MonoBehaviour
{
    public Text GameLobbyTitle;
    public Text IsAnonymous;
    public Text IsRanked;
    public Text Creator;
    public Text MinutesPerTick;
    public Text PlayerList;
    public Text MaxPlayers;
    
    // Start is called before the first frame update
    void Start()
    {
        this.displayLobbyInformation();
    }

    public void displayLobbyInformation()
    {
        GameConfiguration room = ApplicationState.currentGameConfig;

        GameLobbyTitle.text = room.RoomName;
        IsAnonymous.text = room.GameSettings.Anonymous ? "Anonymous" : "";
        IsRanked.text = room.GameSettings.IsRanked ? "Ranked" : "Unrated";
        Creator.text = room.Creator.Username;
        MinutesPerTick.text = String.Format("{0}min/tick \n\nEstimated {1}days total", room.GameSettings.MinutesPerTick, Math.Round((room.GameSettings.MinutesPerTick * 700 / 60.0 / 24.0), 2));
        MaxPlayers.text = $"Players ({room.Players.Count}/{room.GameSettings.MaxPlayers})";

        PlayerList.text = "";
        foreach (User user in room.Players)
        {
            PlayerList.text = PlayerList.text + "\n" + user.Username;
        }
    }
}
