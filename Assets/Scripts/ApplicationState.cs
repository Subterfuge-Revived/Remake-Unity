using System;
using System.Collections;
using System.Collections.Generic;
using Rooms.Multiplayer;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Config;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;

public static class ApplicationState
{
    public static Game CurrentGame { get; set; }
    public static Room currentGameRoom { get; set; }
    
    public static NetworkClient Client { get; } = new NetworkClient();
    public static Player player { get; set; } = null; // Default player

    public static void SetActiveRoom(Room room)
    {
        currentGameRoom = room;
        List<Player> gamePlayers = new List<Player>();
        foreach (User user in room.Players)
        {
            gamePlayers.Add(new Player(user.Id, user.Username));
        }
        
        MapConfiguration mapConfiguration = new MapConfiguration(gamePlayers);
        mapConfiguration.Seed = room.Seed;
        mapConfiguration.DormantsPerPlayer = 3;
        mapConfiguration.MaxiumumOutpostDistance = 140;
        mapConfiguration.MinimumOutpostDistance = 30;
        
        GameConfiguration config = new GameConfiguration(gamePlayers, DateTime.FromFileTimeUtc(room.UnixTimeStarted), mapConfiguration);
        
        CurrentGame = new Game(config);
    }
}
