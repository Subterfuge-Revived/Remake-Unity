using System.Collections;
using System.Collections.Generic;
using Rooms.Multiplayer;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;

public static class ApplicationState
{
    public static Game CurrentGame { get; set; }
    public static Room currentGameRoom { get; set; }
    
    public static NetworkClient Client { get; } = new NetworkClient();
    public static Player player { get; set; }

    public static void SetActiveRoom(Room room)
    {
        currentGameRoom = room;
        List<Player> gamePlayers = new List<Player>();
        foreach (User user in room.Players)
        {
            gamePlayers.Add(new Player(user.Id, user.Username));
        }
        
        GameConfiguration config = new GameConfiguration(gamePlayers);
        config.Seed = room.Seed;
        config.DormantsPerPlayer = 3;
        config.MaxiumumOutpostDistance = 140;
        config.MinimumOutpostDistance = 30;
        
        CurrentGame = new Game(config);
    }
}
