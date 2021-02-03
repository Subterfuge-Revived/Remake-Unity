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
    private SubterfugeClient.SubterfugeClient client = ApplicationState.Client.getClient();

    // Start is called before the first frame update
    async void Start()
    {
        LoadOpenRooms();
    }

    public async void LoadOpenRooms()
    {
        var roomResponse = client.GetOpenLobbies(new OpenLobbiesRequest());

        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            if (gameRoomButton.isActiveAndEnabled)
            {
                Destroy(gameRoomButton);
            }
        }

        if (roomResponse.Status.IsSuccess)
        {

            foreach (Room room in roomResponse.Rooms)
            {
                // Create a new templated item
                GameRoomButton scrollItem = (GameRoomButton) Instantiate(scrollItemTemplate);
                scrollItem.gameObject.SetActive(true);
                scrollItem.room = room;
                // scrollItem.GetComponent<Button>().onClick.AddListener(delegate { GoToGameLobby(room); });

                foreach (Transform child in scrollItem.transform)
                {
                    child.gameObject.SetActive(true); // or false
                }

                // Set the text
                Text text = scrollItem.GetComponentInChildren<Text>();
                text.gameObject.SetActive(true);
                if (text != null)
                {
                    text.text = "[ GameId: " + room.RoomId + " Title: " + room.RoomName + ", Seed: " + room.Seed +
                                ", Players: " + room.Players.Count + "/" + room.MaxPlayers + ", Anonymous: " +
                                room.Anonymous + ", Created By: " + room.Creator.Username + "]";
                }
                else
                {
                    Debug.Log("No Text.");
                }

                // Set the button's parent to the scroll item template.
                scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);

            }

            // TODO: Add some text to notify user they are offline.
        }
    }

    public async void LoadOngoingRooms()
    {
        var roomResponse = client.GetPlayerCurrentGames(new PlayerCurrentGamesRequest());
        
        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            if (gameRoomButton.isActiveAndEnabled)
            {
                Destroy(gameRoomButton.gameObject);
            }
        }

        if (roomResponse.Status.IsSuccess)
        {

            foreach (Room room in roomResponse.Games)
            {
                // Create a new templated item
                GameRoomButton scrollItem = (GameRoomButton) Instantiate(scrollItemTemplate);
                scrollItem.gameObject.SetActive(true);
                scrollItem.room = room;
                scrollItem.GetComponent<Button>().onClick.AddListener(delegate { GoToGame(room); });

                // Set the text
                Text text = scrollItem.GetComponentInChildren<Text>();
                if (text != null)
                {
                    text.text = "[ GameId: " + room.RoomId + " Title: " + room.RoomName + ", Seed: " + room.Seed +
                                ", Players: " + room.Players.Count + "/" + room.MaxPlayers + ", Anonymous: " +
                                room.Anonymous + ", Created By: " + room.Creator.Username + "]";
                }
                else
                {
                    Debug.Log("No Text.");
                }

                // Set the button's parent to the scroll item template.
                scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);
            }
        }
        // TODO: Add some text to notify the user that they are offline.
    }

    public Button.ButtonClickedEvent GoToGameLobby(Room room)
    {
        // Set the gameroom to the selected game
        ApplicationState.currentGameRoom = room;
        
        // Load the game scene
        SceneManager.LoadScene("GameLobby");
        return null;
    }

    public Button.ButtonClickedEvent GoToGame(Room room)
    {
        // Set the gameroom to the selected game
        ApplicationState.SetActiveRoom(room);

        // Load the game scene
        SceneManager.LoadScene("Game");
        return null;
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
