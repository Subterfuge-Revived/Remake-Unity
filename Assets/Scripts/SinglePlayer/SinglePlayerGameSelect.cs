using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Config;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Players;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerGameSelect : MonoBehaviour
{
    public GameRoomButton scrollItemTemplate;

    private List<Player> players = new List<Player>()
    {
        ApplicationState.player,    // First player in the puzzles is the player controlled
        new Player("2"),
        new Player("3"),
        new Player("4"),
        new Player("5"),
        new Player("6"),
    };

    // Start is called before the first frame update
    void Start()
    {
        MapConfiguration mapConfiguration = new MapConfiguration(players);
        mapConfiguration.Seed = 123123;
        mapConfiguration.DormantsPerPlayer = 3;
        mapConfiguration.MaxiumumOutpostDistance = 130;
        mapConfiguration.MinimumOutpostDistance = 30;
        mapConfiguration.OutpostsPerPlayer = 3;
        
        
        GameConfiguration config = new GameConfiguration(players, DateTime.Now, mapConfiguration);

        Dictionary<String, GameConfiguration> puzzleList = new Dictionary<string, GameConfiguration>();
        puzzleList.Add("SampleGameNoNetwork", config);
        
        
        foreach(var puzzle in puzzleList)
        {
            // Create a new templated item
            GameRoomButton scrollItem = (GameRoomButton) Instantiate(scrollItemTemplate);
            scrollItem.gameObject.SetActive(true);
            scrollItem.GetComponent<Button>().onClick.AddListener(delegate { GoToGameLobby(puzzle.Value); });

            // Set the text
            Text text = scrollItem.GetComponentInChildren<Text>();
            text.text = puzzle.Key;

            // Set the button's parent to the scroll item template.
            scrollItem.transform.SetParent(scrollItemTemplate.transform.parent, false);
        }
    }

    private void GoToGameLobby(GameConfiguration config)
    {
        ApplicationState.CurrentGame = new Game(config);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
