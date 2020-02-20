using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Network;
using SubterfugeCore.Core.Players;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckUserLogin : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        string username = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");
        
        if (username != null && password != null && username != "" && password != "")
        {
            // TODO: Set a loading indicator variable here to let the user know that we are trying to log them in automatically.
            
            // Try to login.
            gameObject.AddComponent<Api>();
            Api api = gameObject.GetComponent<Api>();
            LoginResponse response = await api.Login(username, password);
            if (response.success)
            {
                // Go to the main menu.
                PlayerPrefs.SetString("token", response.token);
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                // TODO: Remove the loading indicator and allow the user to enter their credentials.
            }
        }
    }
}
