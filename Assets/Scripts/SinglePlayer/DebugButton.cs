using System;
using System.Collections.Generic;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core.Players;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rooms.Multiplayer.SinglePlayer
{
    public class DebugButton : MonoBehaviour
    {
        public void GoToDebug()
        {
            MapConfiguration mapConfiguration = new MapConfiguration()
            {
                DormantsPerPlayer = 3,
                MinimumOutpostDistance = 30,
                MaximumOutpostDistance = 130,
                OutpostDistribution = new OutpostDistribution()
                {
                    FactoryWeight = 0.40f,
                    GeneratorWeight = 0.40f,
                    WatchtowerWeight = 0.20f,
                },
                OutpostsPerPlayer = 3,
                Seed = 123123,
            
            };
        
            GameConfiguration config = new GameConfiguration()
            {
                Creator = ApplicationState.player.PlayerInstance.ToUser(),
                GameSettings = new GameSettings()
                {
                    IsAnonymous = false,
                    Goal = Goal.Domination,
                    IsRanked = false,
                    MaxPlayers = 6,
                    MinutesPerTick = 10,
                },
                Id = Guid.NewGuid().ToString(),
                MapConfiguration = mapConfiguration,
                RoomName = "LocalRoom",
                RoomStatus = RoomStatus.Ongoing,
                TimeCreated = DateTime.UtcNow,
                TimeStarted = DateTime.UtcNow,
                PlayersInLobby = new List<User>()
                {
                    ApplicationState.player.PlayerInstance.ToUser(),    // First player in the puzzles is the player controlled
                    new SimpleUser() { Id = "2", Username = "2"}.ToUser(),
                    new SimpleUser() { Id = "3", Username = "3"}.ToUser(),
                    new SimpleUser() { Id = "4", Username = "4"}.ToUser(),
                    new SimpleUser() { Id = "5", Username = "5"}.ToUser(),
                    new SimpleUser() { Id = "6", Username = "6"}.ToUser(),
                }
            };
        
            ApplicationState.CurrentGame = new Subterfuge.Remake.Core.Game(config);
            ApplicationState.currentGameConfig = config;
            SceneManager.LoadScene("Scenes/Game");
        }
    }
}