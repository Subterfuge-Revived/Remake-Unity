using System.Collections.Generic;
using SubterfugeCore.Models.GameEvents;
using SubterfugeRestApiClient;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectController : MonoBehaviour
{

    public GameRoomButton scrollItemTemplate;
    public GameObject scrollContentContainer;
    private SubterfugeClient client = ApplicationState.Client.getClient();
    private List<GameRoomButton> _instantiatedRooms = new List<GameRoomButton>();

    public void Start()
    {
        LoadOpenRooms();
    }

    private void clearLobbyList()
    {
        // Destroy all existing rooms.
        foreach (GameRoomButton gameRoomButton in _instantiatedRooms)
        {
            Destroy(gameRoomButton.gameObject);
        }
    }

    private void instantiateRoomButton(GameConfiguration roomConfig)
    {
        // Create a new templated item
        GameRoomButton scrollItem = (GameRoomButton) Instantiate(scrollItemTemplate, scrollContentContainer.transform);
        scrollItem.gameObject.SetActive(true);
        scrollItem.setGameRoom(roomConfig);
        _instantiatedRooms.Add(scrollItem);
    }

    public async void LoadOpenRooms()
    {
        var roomResponse = await client.LobbyClient.GetLobbies( new GetLobbyRequest(){ RoomStatus = RoomStatus.Open });

        clearLobbyList();

        if (roomResponse.Status.IsSuccess)
        {
            foreach (GameConfiguration room in roomResponse.Lobbies)
            {
                instantiateRoomButton(room);
            }

            // TODO: Add some text to notify user they are offline.
        }
    }

    public async void LoadOngoingRooms()
    {
        var roomResponse = await client.LobbyClient.GetLobbies(new GetLobbyRequest(){ UserIdInRoom = ApplicationState.player.GetId() });
        
        clearLobbyList();

        if (roomResponse.Status.IsSuccess)
        {

            foreach (GameConfiguration room in roomResponse.Lobbies)
            {
                instantiateRoomButton(room);
            }
        }
        // TODO: Add some text to notify the user that they are offline.
    }
    
    public void createPublicGame()
    {
        SceneManager.LoadScene("CreateGame");
    }
    
    public void createPrivateGame()
    {
        // TODO
        SceneManager.LoadScene("CreateGame");
    }

    public void onBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
