using Rooms.Multiplayer;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Players;
using SubterfugeCore.Models.GameEvents;

public static class ApplicationState
{
    public static Game CurrentGame { get; set; }
    public static GameConfiguration currentGameConfig { get; set; }
    public static bool isMultiplayer { get; set; }
    
    
    public static NetworkClient Client { get; } = new NetworkClient();
    public static Player player { get; set; } = null; // Default player

    public static void SetActiveRoom(GameConfiguration room)
    {
        currentGameConfig = room;
        if (room.GameSettings.MaxPlayers == room.PlayersInLobby.Count)
        {
            CurrentGame = new Game(currentGameConfig);
        }
    }
}
