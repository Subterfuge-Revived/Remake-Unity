using System;
using System.Collections.Generic;
using System.Linq;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core;
using Subterfuge.Remake.Core.Players;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        // Setup a single player game.
        List<Player> players = new List<Player>();
        players.Add(ApplicationState.player);
        players.Add(new Player(new SimpleUser() { Id = "2", Username = "2"}));
        players.Add(new Player(new SimpleUser() { Id = "3", Username = "3" }));
        players.Add(new Player(new SimpleUser() { Id = "4", Username = "4" }));
        
        
        GameConfiguration config = new GameConfiguration()
        {
            Creator = ApplicationState.player.PlayerInstance.ToUser(),
            GameSettings = new GameSettings()
            {
                IsAnonymous = false,
                Goal = Goal.Mining,
                IsRanked = false,
                MaxPlayers = 4,
                MinutesPerTick = 10,
            },
            Id = Guid.NewGuid().ToString(),
            MapConfiguration = new MapConfiguration()
            {
                DormantsPerPlayer = 4,
                MinimumOutpostDistance = 50,
                MaximumOutpostDistance = 210,
                OutpostDistribution = new OutpostDistribution()
                {
                    FactoryWeight = 0.40f,
                    GeneratorWeight = 0.40f,
                    WatchtowerWeight = 0.20f,
                },
                OutpostsPerPlayer = 3,
                Seed = 169081503,
            },
            RoomName = "LocalRoom",
            RoomStatus = RoomStatus.Ongoing,
            TimeCreated = DateTime.UtcNow,
            TimeStarted = DateTime.UtcNow,
        };
        config.PlayersInLobby.AddRange(players.Select(player => player.PlayerInstance.ToUser()));
        
        Game game = new Game(config);
        ApplicationState.CurrentGame = game;
        ApplicationState.isMultiplayer = false;
        
        SceneManager.LoadScene("Game");
    }
}
