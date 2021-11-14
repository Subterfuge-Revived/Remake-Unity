using SubterfugeRemakeService;
using UnityEngine;

namespace Rooms.Multiplayer.GameLobby
{
    public class PlayerListLoader : MonoBehaviour
    {
        public GameObject scrollContent;
        public PlayerProfile scrollTemplate;
        
        public void Start()
        {
            foreach (User user in ApplicationState.currentGameConfig.Players)
            {
                var player = Instantiate(scrollTemplate, scrollContent.transform);
                player.gameObject.SetActive(true);
                player.setUser(user);
            }
        }
    }
}