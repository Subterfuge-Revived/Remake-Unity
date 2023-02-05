using UnityEngine;

namespace Rooms.Multiplayer.GameLobby
{
    public class PlayerListLoader : MonoBehaviour
    {
        public GameObject scrollContent;
        public PlayerProfile scrollTemplate;
        
        public void Start()
        {
            foreach (var user in ApplicationState.currentGameConfig.PlayersInLobby)
            {
                var player = Instantiate(scrollTemplate, scrollContent.transform);
                player.gameObject.SetActive(true);
                player.setUser(user);
            }
        }
    }
}