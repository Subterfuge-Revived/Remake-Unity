using SubterfugeRemakeService;
using TMPro;
using UnityEngine;

namespace Rooms.Multiplayer.GameLobby
{
    public class PlayerProfile : MonoBehaviour
    {

        public TextMeshProUGUI playerName;

        public void setUser(User user)
        {
            playerName.text = user.Username;
        }

    }
}