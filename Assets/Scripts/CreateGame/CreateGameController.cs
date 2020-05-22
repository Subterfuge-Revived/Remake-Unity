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

    public Api api = new Api();

    public async void onCreateGame()
    {
        NetworkResponse<CreateLobbyResponse> response = await api.CreateLobby(gameTitle.text, (int)playerCount.value, 0, rankedToggle.isOn, anonToggle.isOn, "neptunium-200", 0);

        if (response.IsSuccessStatusCode())
        {
            SceneManager.LoadScene("GameSelect");
        }
        else
        {
            // TODO: Tell the user that their request failed. If error, show message, if timeout say offline.
            // If offline, potentially add their requests to a queue.
        }
    }

    public void onCancel()
    {
        SceneManager.LoadScene("GameSelect");
    }
    
    
}
