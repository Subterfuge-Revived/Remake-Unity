using Rooms.Multiplayer.CreateGame;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rooms.Multiplayer.GameLobby
{
    public class GameLobbyController : MonoBehaviour
    {

        public TextMeshProUGUI GameLobbyName;
        public TextMeshProUGUI GameLobbyAuthor;

        public GeneralConfigController GeneralConfigController;
        public MapConfigController MapConfigController;
        public void Start()
        {
            GameLobbyName.text = ApplicationState.currentGameConfig.RoomName;
            GameLobbyAuthor.text = ApplicationState.currentGameConfig.Creator.Username;
            
            // Load values from configuration value.
            GeneralConfigController.loadFromConfig(ApplicationState.currentGameConfig.GameSettings);
            MapConfigController.loadFromConfig(ApplicationState.currentGameConfig.MapConfiguration);
        }
        
        public void back()
        {
            SceneManager.LoadScene("GameSelect");
        }
        
    }
}