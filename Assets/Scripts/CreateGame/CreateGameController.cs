using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGameController : MonoBehaviour
{

    public Slider playerCount;
    public Text gameTitle;
    public Toggle rankedToggle;
    public Toggle anonToggle;

    public Api api;
    // Start is called before the first frame update
    void Start()
    {
        api = gameObject.GetComponent<Api>();
    }

    public async void onCreateGame()
    {
        CreateLobbyResponse response =
            await api.CreateLobby(gameTitle.text, (int)playerCount.value, 0, rankedToggle.isOn, anonToggle.isOn, 0, 0);
        
        SceneManager.LoadScene("GameSelect");
    }

    public void onCancel()
    {
        SceneManager.LoadScene("GameSelect");
    }
    
    
}
