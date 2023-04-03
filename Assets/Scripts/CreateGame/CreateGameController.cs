using Rooms.Multiplayer.CreateGame;
using Subterfuge.Remake.Api.Network;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateGameController : MonoBehaviour
{
    public GeneralConfigController generalConfig;
    public MapConfigController mapConfig;

    public TMP_InputField gameTitle;

    public async void onCreateGame()
    {
        var generalConfiguration = this.generalConfig.getConfiguredValues();
        var mapConfiguration = mapConfig.getConfiguredValues();

        var client = ApplicationState.Client.getClient();
        var request = new CreateRoomRequest()
        {
            GameSettings = generalConfiguration,
            IsPrivate = false,
            MapConfiguration = mapConfiguration,
            RoomName = gameTitle.text,
        };
        
        (await client.LobbyClient.CreateNewRoom(request)).Get(
            (success) =>
            {
                SceneManager.LoadScene("GameSelect");
            },
            (failure) =>
            {
                // TODO: Tell the user that their request failed. If error, show message, if timeout say offline.
                // If offline, potentially add their requests to a queue.
            }
        );
    }

    public void onCancel()
    {
        SceneManager.LoadScene("GameSelect");
    }
    
}
