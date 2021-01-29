using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Network;
using SubterfugeCore.Core.Players;
using UnityEngine;

public static class ApplicationState
{
    public static GameRoom currentGameRoom { get; set; }
    public static Player player { get; set; }
}
