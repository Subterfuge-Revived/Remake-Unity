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
    public static GameConfiguration currentGameConfig { get; set; }
    public static bool isMultiplayer { get; set; }
    
    
    public static NetworkClient Client { get; } = new NetworkClient();
    public static Player player { get; set; } = null; // Default player

    public static void SetActiveRoom(GameConfiguration room)
    {
        currentGameConfig = room;
        CurrentGame = new Game(currentGameConfig);
    }
}
