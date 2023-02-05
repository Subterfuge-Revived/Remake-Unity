using System;
using System.Collections.Generic;
using System.Linq;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Players;
using SubterfugeCore.Models.GameEvents;
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
        players.Add(new Player("2"));
        players.Add(new Player("3"));
        players.Add(new Player("4"));
        
        
        GameConfiguration config = new GameConfiguration()
        {
            Creator = ApplicationState.player.ToUser(),
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
        config.PlayersInLobby.AddRange(players.Select(player => player.ToUser()));
        
        Game game = new Game(config);
        ApplicationState.CurrentGame = game;
        ApplicationState.isMultiplayer = false;
        
        SceneManager.LoadScene("Game");
    }
}
