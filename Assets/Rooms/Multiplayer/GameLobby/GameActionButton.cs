using SubterfugeRemakeService;
using Translation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameActionButton : MonoBehaviour
{
    public Button actionButton;
    
    // Start is called before the first frame update
    void Start()
    {
        // Determine if the current user is the creator of the game
        Room room = ApplicationState.currentGameRoom;
        if (room.Creator.Id == ApplicationState.player.GetId())
        {
            // Determine if there are more players than just the creator.
            if (room.Players.Count > 1)
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = StringFactory.GetString(GameString.GameLobby_Button_StartEarly);
                actionButton.onClick.AddListener(onStartEarly);
            }
            else
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = StringFactory.GetString(GameString.GameLobby_Label_WaitingForPlayers);
                actionButton.interactable = false;
            }
        }
        else
        {
            bool isInGame = false;
            foreach(User player in room.Players)
            {
                Debug.unityLogger.Log(ApplicationState.player.GetId());
                Debug.unityLogger.Log(room.Players);
                if (!isInGame && player.Id == ApplicationState.player.GetId())
                {
                    isInGame = true;
                }
            }

            if (isInGame)
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = StringFactory.GetString(GameString.GameLobby_Label_WaitingForPlayers);
                actionButton.interactable = false;                
            }
            else
            {
                // Show start game early button.
                Text buttonText = actionButton.GetComponentInChildren<Text>();
                buttonText.text = StringFactory.GetString(GameString.GameLobby_Button_JoinGame);
                actionButton.onClick.AddListener(onJoinLobby);
            }
        }
    }

    public async void onJoinLobby()
    {
        var client = ApplicationState.Client.getClient();
        var joinResponse = client.JoinRoom(new JoinRoomRequest()
        {
            RoomId = ApplicationState.currentGameRoom.RoomId
        });

        if (joinResponse.Status.IsSuccess)
        {
            User user = new User();
            user.Id = ApplicationState.player.GetId();
            user.Username = ApplicationState.player.GetPlayerName();
        
            ApplicationState.currentGameRoom.Players.Add(user);
            
            if(ApplicationState.currentGameRoom.Players.Count == ApplicationState.currentGameRoom.MaxPlayers)
            {
                SceneManager.LoadScene("Game");
            }
        
            // Reload the scene to update lobby.
            SceneManager.LoadScene("GameLobby");   
        }
        else
        {
            Debug.Log(joinResponse);
            Debug.Log("Unable to join game");
            // TODO: Add some text to notify the user they are offline.
            // Potentially add the user's request to a queue that gets attempted when they regain connectivity.
        }
    }
    
    public async void onStartEarly()
    {
        
        var client = ApplicationState.Client.getClient();
        var startEarlyResponse = client.StartGameEarly(new StartGameEarlyRequest()
        {
            RoomId = ApplicationState.currentGameRoom.RoomId,
        });

        if (startEarlyResponse.Status.IsSuccess)
        {
            // Load the config.
            SceneManager.LoadScene("Game");
        }
        else
        {
            // TODO: Tell the user that they are offline or send the error message.
            // If offline, potentailly add their request to a queue.
            
            // Reload the scene to update lobby. Handle error here.
            SceneManager.LoadScene("GameLobby");
        }
    }

    public async void onCancel()
    {
        ApplicationState.currentGameRoom = null;
        SceneManager.LoadScene("GameSelect");
    }
}
