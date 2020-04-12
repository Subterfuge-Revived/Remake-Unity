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
        text.text = "Logged in as: " + username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
