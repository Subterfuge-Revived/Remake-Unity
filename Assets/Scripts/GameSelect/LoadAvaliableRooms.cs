using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAvaliableRooms : MonoBehaviour
{

    public GameRoomButton scrollItemTemplate;
    public GameObject scrollContentContainer;
    private SubterfugeClient.SubterfugeClient client = ApplicationState.Client.getClient();

    // Start is called before the first frame update
    async void Start()
    {
        LoadOpenRooms();
    }

    private void clearLobbyList()
    {
        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            if (gameRoomButton.isActiveAndEnabled)
            {
                Destroy(gameRoomButton);
            }
        }
    }

    private void instantiateRoomButton(Room room)
    {
        // Create a new templated item
        GameRoomButton scrollItem = (GameRoomButton) Instantiate(scrollItemTemplate, scrollContentContainer.transform);
        scrollItem.gameObject.SetActive(true);
        scrollItem.room = room;
    }

    public async void LoadOpenRooms()
    {
        var roomResponse = client.GetOpenLobbies(new OpenLobbiesRequest());

        clearLobbyList();

        if (roomResponse.Status.IsSuccess)
        {
            foreach (Room room in roomResponse.Rooms)
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

            foreach (Room room in roomResponse.Games)
            {
                instantiateRoomButton(room);
            }
        }
        // TODO: Add some text to notify the user that they are offline.
    }
    
    public void onCreateGameClicked()
    {
        SceneManager.LoadScene("CreateGame");
    }

    public void onBackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
