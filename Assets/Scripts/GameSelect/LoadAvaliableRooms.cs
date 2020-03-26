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
    private Api api = null;

    // Start is called before the first frame update
    async void Start()
    {
        gameObject.AddComponent<Api>();
        LoadOpenRooms();
    }

    public async void LoadOpenRooms()
    {
        api = gameObject.GetComponent<Api>();
        List<GameRoom> roomResponse = await api.GetOpenRooms();
        
        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            Destroy(gameRoomButton.gameObject);
        }

        foreach(GameRoom room in roomResponse)
        {
            // Create a new templated item
            GameRoomButton scrollItem = (GameRoomButton)Instantiate(scrollItemTemplate);
            scrollItem.gameObject.SetActive(true);
            scrollItem.room = room;
            scrollItem.GetComponent<Button>().onClick.AddListener(delegate { goToGameLobby(room); });
            
            // Set the text
            Text text = scrollItem.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = "[ GameId: " + room.room_id + " Title: " + room.description + ", Seed: " + room.seed + ", Players: " + room.players.Count + ", Anonymous: " + room.anonimity + ", Created By: " + room.creator_id + "]";
            }
            else
            {
                Debug.Log("No Text.");
            }

            // Set the button's parent to the scroll item template.
            scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);
        }
    }
    
    public async void LoadOngoingRooms()
    {
        api = gameObject.GetComponent<Api>();
        List<GameRoom> roomResponse = await api.GetOngoingRooms();
        
        // Destroy all existing rooms.
        GameRoomButton[] existingButtons = FindObjectsOfType<GameRoomButton>();
        foreach (GameRoomButton gameRoomButton in existingButtons)
        {
            Destroy(gameRoomButton.gameObject);
        }

        foreach(GameRoom room in roomResponse)
        {
            // Create a new templated item
            GameRoomButton scrollItem = (GameRoomButton)Instantiate(scrollItemTemplate);
            scrollItem.gameObject.SetActive(true);
            scrollItem.room = room;
            scrollItem.GetComponent<Button>().onClick.AddListener(delegate { goToGame(room); });
            
            // Set the text
            Text text = scrollItem.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = "[ GameId: " + room.room_id + " Title: " + room.description + ", Seed: " + room.seed + ", Players: " + room.players.Count + ", Anonymous: " + room.anonimity + ", Created By: " + room.creator_id + "]";
            }
            else
            {
                Debug.Log("No Text.");
            }

            // Set the button's parent to the scroll item template.
            scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);
        }
    }

    public Button.ButtonClickedEvent goToGameLobby(GameRoom room)
    {
        // Set the gameroom to the selected game
        ApplicationState.currentGameRoom = room;
        
        // Load the game scene
        SceneManager.LoadScene("GameLobby");
        return null;
    }

    public Button.ButtonClickedEvent goToGame(GameRoom room)
    {
        // Set the gameroom to the selected game
        ApplicationState.currentGameRoom = room;
        
        // Load the game scene
        SceneManager.LoadScene("Game");
        return null;
    }
}
