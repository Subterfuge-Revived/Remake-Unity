using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Config;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Players;
using UnityEditor.VersionControl;
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
        
        MapConfiguration mapConfiguration = new MapConfiguration(players);
        mapConfiguration.Seed = 169081503;
        mapConfiguration.DormantsPerPlayer = 4;
        mapConfiguration.MaxiumumOutpostDistance = 210;
        mapConfiguration.MinimumOutpostDistance = 50;
        mapConfiguration.OutpostsPerPlayer = 3;
        
        
        GameConfiguration config = new GameConfiguration(players, DateTime.Now, mapConfiguration);
        
        Game game = new Game(config);
        ApplicationState.CurrentGame = game;
        
        SceneManager.LoadScene("Game");
    }
}
