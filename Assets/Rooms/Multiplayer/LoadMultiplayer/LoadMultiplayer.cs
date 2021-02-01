using System;
using System.Collections;
using System.Collections.Generic;
using Grpc.Core;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMultiplayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // String Hostname = "server"; // For docker
        String Hostname = "localhost"; // For local
        int Port = 5000;

        var client = new SubterfugeClient.SubterfugeClient(Hostname, Port.ToString());
        
        // Ensure that the client can connect to the server.
        try
        {
            client.HealthCheck(new HealthCheckRequest());
        }
        catch (RpcException exception)
        {
            // Show error saying that the user cannot connect to the game servers.
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
                    SceneManager.LoadScene("GameSelect");
                }
            }
            catch (RpcException e)
            {
                Debug.Log(e);
                SceneManager.LoadScene("LoginScreen");
            }
        }
        // Show player the login screen.
        SceneManager.LoadScene("LoginScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
