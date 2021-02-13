using System;
using System.Collections;
using System.Collections.Generic;
using Grpc.Core;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMultiplayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (ApplicationState.Client.isConnected)
        {
            var client = ApplicationState.Client.getClient();
            // Ensure that the client can connect to the server.
            try
            {
                client.HealthCheck(new HealthCheckRequest());
            }
            catch (RpcException exception)
            {
                // Show error saying that the user cannot connect to the game servers.
                Debug.Log("Cannot connect to server.");
                SceneManager.LoadScene("Rooms/MainMenu/MainMenu");
            }
            
            // If connected, verify user login:
            var token = PlayerPrefs.GetString("token");
            if (token != null)
            {
                try
                {
                    var response = client.LoginWithToken(new AuthorizedTokenRequest()
                    {
                        Token = token,
                    });
         
                    if (response?.User?.Username != null)
                    {
                        // Take user to multiplayer lobby
                        PlayerPrefs.SetString("username", response.User.Username);
                        ApplicationState.player = new Player(response.User);
                        SceneManager.LoadScene("GameSelect");
                    }
                }
                catch (RpcException e)
                {
                    // Unable to login with token, let the user login using their credentials instead.
                    SceneManager.LoadScene("LoginScreen");
                }
            }
            else
            {
                SceneManager.LoadScene("LoginScreen");
            }
        }
        else
        {
            // Show error saying that the user cannot connect to the game servers.
            Debug.Log("Cannot connect to server.");
            SceneManager.LoadScene("Rooms/MainMenu/MainMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
