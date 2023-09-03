using System;
using System.Collections.Generic;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core;
using Subterfuge.Remake.Core.Players;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerGameSelect : MonoBehaviour
{
    public GameRoomButton scrollItemTemplate;

    private List<Player> players = new List<Player>()
    {
        ApplicationState.player,    // First player in the puzzles is the player controlled
        new Player(new SimpleUser() { Id = "2", Username = "2"}),
        new Player(new SimpleUser() { Id = "3", Username = "3"}),
        new Player(new SimpleUser() { Id = "4", Username = "4"}),
        new Player(new SimpleUser() { Id = "5", Username = "5"}),
        new Player(new SimpleUser() { Id = "6", Username = "6"}),
    };

    // Start is called before the first frame update
    void Start()
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
            TimeStarted = DateTime.UtcNow
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
            TimeStarted = DateTime.UtcNow
        };
        
        ApplicationState.CurrentGame = new Game(config);
        SceneManager.LoadScene("Scenes/Game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
