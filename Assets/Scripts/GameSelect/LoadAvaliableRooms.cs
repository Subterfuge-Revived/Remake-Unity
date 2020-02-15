using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.UI;

public class LoadAvaliableRooms : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        gameObject.AddComponent<Api>();
        Api api = gameObject.GetComponent<Api>();
        List<GameRoom> roomResponse = await api.GetOpenRooms();

        string rooms = "";
        foreach(GameRoom room in roomResponse)
        {
            rooms += "[ descrip: " + room.description + ", seed: " + room.seed + "]";
        }
        gameObject.GetComponent<Text>().text = rooms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
