using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnlinePlay()
    {
        // Check if connected to network and logged in first.
        if (ApplicationState.Client.isConnected && ApplicationState.player != null)
        {
            SceneManager.LoadScene("GameSelect");
        }
        else
        {
            // TODO: Show an error.
        }
    }
}
