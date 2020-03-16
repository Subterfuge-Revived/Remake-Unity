using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.UI;

public class LoadAvaliableRooms : MonoBehaviour
{

    public Button scrollItemTemplate;

    // Start is called before the first frame update
    async void Start()
    {
        gameObject.AddComponent<Api>();
        Api api = gameObject.GetComponent<Api>();
        List<GameRoom> roomResponse = await api.GetOpenRooms();

        string rooms = "";
        foreach(GameRoom room in roomResponse)
        {
            // Create a new templated item
            Button scrollItem = (Button)Instantiate(scrollItemTemplate);
            scrollItem.gameObject.SetActive(true);
            
            // Set the text
            Text text = scrollItem.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = "[ descrip: " + room.description + ", seed: " + room.seed + "]";
            }
            else
            {
                Debug.Log("No Text.");
            }

            Debug.Log("[ descrip: " + room.description + ", seed: " + room.seed + "]");

            // Set the button's parent to the scroll item template.
            scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);
            
            // rooms += "[ descrip: " + room.description + ", seed: " + room.seed + "]";
        }
        // gameObject.GetComponent<Text>().text = rooms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
