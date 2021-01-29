using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Players;
using UnityEngine;
using UnityEngine.UI;

public class LoadUser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string username = PlayerPrefs.GetString("username");
        Text text = gameObject.GetComponent<Text>();
        if (username != null)
        {
            text.text = "Logged in as: " + username;
        }
        else
        {
            text.text = "Not logged in.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
