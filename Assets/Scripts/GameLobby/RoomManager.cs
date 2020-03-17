using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onJoinLobby()
    {
        SceneManager.LoadScene("Game");
    }

    public void onCancel()
    {
        SceneManager.LoadScene("GameSelect");
    }
}
