using Rooms.Multiplayer;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core;
using Subterfuge.Remake.Core.Players;

public static class ApplicationState
{
    public static Game CurrentGame { get; set; }
    public static GameConfiguration currentGameConfig { get; set; }
    public static bool isMultiplayer { get; set; }
    
    
    public static NetworkClient Client { get; } = new NetworkClient();
    public static Player player { get; set; } = new Player(new SimpleUser() { Id = "DebugUser", Username = "DebugUser"}); // Default player

    public static void SetActiveRoom(GameConfiguration room)
    {
        currentGameConfig = room;
        if (room.GameSettings.MaxPlayers == room.PlayersInLobby.Count)
        {
            CurrentGame = new Game(currentGameConfig);
        }
    }
}
