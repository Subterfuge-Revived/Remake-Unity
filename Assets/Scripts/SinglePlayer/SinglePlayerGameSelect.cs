using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Config;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEditor.UnityLinker;
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
        MapConfiguration mapConfiguration = new MapConfiguration()
        {
            DormantsPerPlayer = 3,
            MinimumOutpostDistance = 30,
            MaximumOutpostDistance = 130,
            OutpostDistribution = new OutpostWeighting()
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
            Creator = ApplicationState.player.toUser(),
            GameSettings = new GameSettings()
            {
                Anonymous = false,
                Goal = Goal.Domination,
                IsRanked = false,
                MaxPlayers = 6,
                MinutesPerTick = 10,
            },
            Id = Guid.NewGuid().ToString(),
            MapConfiguration = mapConfiguration,
            RoomName = "LocalRoom",
            RoomStatus = RoomStatus.Ongoing,
            UnixTimeCreated = DateTime.Now.ToFileTimeUtc(),
            UnixTimeStarted = DateTime.Now.ToFileTimeUtc()
        };

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
