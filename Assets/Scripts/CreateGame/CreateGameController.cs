using System;
using System.Linq;
using Rooms.Multiplayer.CreateGame;
using SubterfugeCore.Core.Timing;
using SubterfugeRemakeService;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CreateGameController : MonoBehaviour
{
    public GeneralConfigController generalConfig;
    public MapConfigController mapConfig;
    public SpecialistConfigController SpecialistConfigController;

    public TMP_InputField gameTitle;

    public async void onCreateGame()
    {
        var generalConfiguration = this.generalConfig.getConfiguredValues();
        var mapConfiguration = mapConfig.getConfiguredValues();
        generalConfiguration.AllowedSpecialists.AddRange(SpecialistConfigController.getConfiguredValues());

        var client = ApplicationState.Client.getClient();
        var request = new CreateRoomRequest()
        {
            GameSettings = generalConfiguration,
            IsPrivate = false,
            MapConfiguration = mapConfiguration,
            RoomName = gameTitle.text,
        };
        
        var response = await client.CreateNewRoomAsync(request);

        if (response.Status.IsSuccess)
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
