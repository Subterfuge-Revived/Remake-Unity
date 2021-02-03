using SubterfugeRemakeService;
using Translation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveGameButton : MonoBehaviour
{
    public Button leaveButton;
    // Start is called before the first frame update
    void Start()
    {
        //Determine if the user in in the game.
        bool isInGame = false;
        foreach(User player in ApplicationState.currentGameRoom.Players)
        {
            if (!isInGame && player.Id == ApplicationState.player.GetId())
            {
                isInGame = true;
            }
        }

        if (isInGame)
        {
            // Determine if the current user is the creator of the game
            Room room = ApplicationState.currentGameRoom;
            if (room.Creator.Id == ApplicationState.player.GetId())
            {
                Text buttonText = leaveButton.GetComponentInChildren<Text>();
                buttonText.text = StringFactory.GetString(GameString.GameLobby_Button_CancelGame);
            }
            else
            {
                Text buttonText = leaveButton.GetComponentInChildren<Text>();
                buttonText.text = StringFactory.GetString(GameString.GameLobby_Button_LeaveGame);
            }
            leaveButton.onClick.AddListener(onLeaveLobby);            
        }
        else
        {
            leaveButton.gameObject.SetActive(false);
        }
    }

    public async void onLeaveLobby()
    {
        var client = ApplicationState.Client.getClient();
        var leaveResponse = client.LeaveRoom(new LeaveRoomRequest()
        {
            RoomId = ApplicationState.currentGameRoom.RoomId
        });

        if (leaveResponse.Status.IsSuccess)
        {
            // Reload the scene to update lobby.
            ApplicationState.currentGameRoom = null;
            SceneManager.LoadScene("GameSelect");
        }
        else
        {
            // TODO: Add some text to tell the user they are offline.
        }
    }
}
