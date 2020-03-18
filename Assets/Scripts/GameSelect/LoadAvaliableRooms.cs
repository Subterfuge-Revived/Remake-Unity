using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.UI;

public class LoadAvaliableRooms : MonoBehaviour
{

    public GameRoomButton scrollItemTemplate;
    private Api api = null;

    // Start is called before the first frame update
    async void Start()
    {
        await LoadGameRooms();
    }

    public async Task LoadGameRooms()
    {
        gameObject.AddComponent<Api>();
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
            
            // Set the text
            Text text = scrollItem.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = "[ Title: " + room.description + ", Seed: " + room.seed + ", Players: " + room.players.Count + ", Anonymous: " + room.anonimity + ", Created By: " + room.creator_id + "]";
            }
            else
            {
                Debug.Log("No Text.");
            }

            // Set the button's parent to the scroll item template.
            scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
