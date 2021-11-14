using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSelectController : MonoBehaviour
{

    public GameRoomButton scrollItemTemplate;
    public GameObject scrollContentContainer;
    private SubterfugeClient.SubterfugeClient client = ApplicationState.Client.getClient();
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
        var roomResponse = await client.GetOpenLobbiesAsync(new OpenLobbiesRequest());

        clearLobbyList();

        if (roomResponse.Status.IsSuccess)
        {
            foreach (GameConfiguration room in roomResponse.Rooms)
            {
                instantiateRoomButton(room);
            }

            // TODO: Add some text to notify user they are offline.
        }
    }

    public async void LoadOngoingRooms()
    {
        var roomResponse = client.GetPlayerCurrentGames(new PlayerCurrentGamesRequest());
        
        clearLobbyList();

        if (roomResponse.Status.IsSuccess)
        {

            foreach (GameConfiguration room in roomResponse.Games)
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
