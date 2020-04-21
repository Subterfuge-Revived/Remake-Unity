using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAvaliableRooms : MonoBehaviour
{

    public GameRoomButton scrollItemTemplate;
    Api api = new Api();

    // Start is called before the first frame update
    async void Start()
    {
        LoadOpenRooms();
    }

    public async void LoadOpenRooms()
    {
        NetworkResponse<List<GameRoom>> roomResponse = await api.GetOpenRooms();

        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            Destroy(gameRoomButton.gameObject);
        }

        if (roomResponse.IsSuccessStatusCode())
        {

            foreach (GameRoom room in roomResponse.Response)
            {
                // Create a new templated item
                GameRoomButton scrollItem = (GameRoomButton) Instantiate(scrollItemTemplate);
                scrollItem.gameObject.SetActive(true);
                scrollItem.room = room;
                scrollItem.GetComponent<Button>().onClick.AddListener(delegate { GoToGameLobby(room); });

                // Set the text
                Text text = scrollItem.GetComponentInChildren<Text>();
                if (text != null)
                {
                    text.text = "[ GameId: " + room.RoomId + " Title: " + room.Description + ", Seed: " + room.Seed +
                                ", Players: " + room.Players.Count + "/" + room.MaxPlayers + ", Anonymous: " +
                                room.Anonimity + ", Created By: " + room.CreatorId + "]";
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
        NetworkResponse<List<GameRoom>> roomResponse = await api.GetOngoingRooms();
        
        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            Destroy(gameRoomButton.gameObject);
        }

        if (roomResponse.IsSuccessStatusCode())
        {

            foreach (GameRoom room in roomResponse.Response)
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
                    text.text = "[ GameId: " + room.RoomId + " Title: " + room.Description + ", Seed: " + room.Seed +
                                ", Players: " + room.Players.Count + "/" + room.MaxPlayers + ", Anonymous: " +
                                room.Anonimity + ", Created By: " + room.CreatorId + "]";
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

    public Button.ButtonClickedEvent GoToGameLobby(GameRoom room)
    {
        // Set the gameroom to the selected game
        ApplicationState.currentGameRoom = room;
        
        // Load the game scene
        SceneManager.LoadScene("GameLobby");
        return null;
    }

    public Button.ButtonClickedEvent GoToGame(GameRoom room)
    {
        // Set the gameroom to the selected game
        ApplicationState.currentGameRoom = room;
        
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
