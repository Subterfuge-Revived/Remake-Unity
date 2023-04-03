using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core;
using Subterfuge.Remake.Core.Entities.Components;
using Subterfuge.Remake.Core.Entities.Positions;
using Subterfuge.Remake.Core.GameEvents.PlayerTriggeredEvents;
using Subterfuge.Remake.Core.Players;
using Subterfuge.Remake.Core.Timing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool showLaunchHud = false;
    public GameObject launchHud;
    public Outpost launchOutpost;
    public Outpost destinationOutpost;
    public Slider drillerSlider;
    public GameObject timeMachineHud;
    
    // Start is called before the first frame update
    async void Start()
    {
        this.SetLaunchHub(false);
        // TODO: Add a configuration to the `Game` object to determine if it is a multiplayer game.
        if (ApplicationState.CurrentGame != null && ApplicationState.isMultiplayer)
        {
            loadMultiplayerGame();
        }
        else
        {
            loadSinglePlayerGame();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the pressed location was an outpost. If it was, the user is trying to launch a sub.
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "Outpost")
            {
                // Clicked object is an outpost, don't move the camera.
                launchOutpost = hit.collider.gameObject.GetComponent<OutpostManager>().outpost;
                return;
            }   
        }
        // If the mouse button is released, apply velocity to the map to scroll
        if (Input.GetMouseButtonUp(0))
        {
            // If the first click was on an outpost, check if the second is on another outpost for a launch.
            if (launchOutpost != null && showLaunchHud == false)
            {
                // Check if the pressed location was an outpost. If it was, the user is trying to launch a sub.
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.tag == "Outpost")
                {
                    // Clicked object is an outpost, don't move the camera.
                    destinationOutpost = hit.collider.gameObject.GetComponent<OutpostManager>().outpost;
                    
                    // only show the hud if the souce outpost is owned by the current player & the destination is not the source.
                    if (launchOutpost != destinationOutpost &&
                        launchOutpost.GetComponent<DrillerCarrier>().GetOwner().GetId() == ApplicationState.player.GetId())
                    {

                        SouceLaunchInformation sourcePanel = launchHud.GetComponentInChildren<SouceLaunchInformation>();
                        sourcePanel.source = launchOutpost;
                        SubLaunchInformation informationPanel = launchHud.GetComponentInChildren<SubLaunchInformation>();
                        informationPanel.destination = destinationOutpost;
                        informationPanel.sourceOutpost = launchOutpost;
                        drillerSlider.maxValue = launchOutpost.GetComponent<DrillerCarrier>().GetDrillerCount();

                        this.SetLaunchHub(true);
                    }
                    else
                    {
                        launchOutpost = null;
                    }
                }
            } else if (showLaunchHud)
            {
                // Determine if the click was in the panel
                if (EventSystem.current.IsPointerOverGameObject()) return;
                this.SetLaunchHub(false);
            }
        }
    }

    public void AdvanceTimemachine(int ticks)
    {
        ApplicationState.CurrentGame.TimeMachine.Advance(ticks);
    }

    public void SetLaunchHub(bool state)
    {
        showLaunchHud = state;
        timeMachineHud.SetActive(!state);
        launchHud.SetActive(state);
    }

    public void launchSub()
    {
        var gameEventData = new GameEventData()
        {
            
            EventDataType = EventDataType.LaunchEventData,
            SerializedEventData = JsonConvert.SerializeObject(new LaunchEventData()
            {
                SourceId = launchOutpost.GetComponent<IdentityManager>().GetId(),
                DestinationId = destinationOutpost.GetComponent<IdentityManager>().GetId(),
                DrillerCount = (int)drillerSlider.value,
                SpecialistIds = { },
            }),
            OccursAtTick = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick().Advance(1).GetTick()
        };
        
        LaunchEvent launchEvent = new LaunchEvent(
            new GameRoomEvent()
            {
                GameEventData = gameEventData,
                Id = "TestEvent",
                IssuedBy = ApplicationState.player.PlayerInstance.ToUser().ToSimpleUser(),
                RoomId = ApplicationState.currentGameConfig.Id,
                TimeIssued = DateTime.Now
            });
        
        ApplicationState.CurrentGame.TimeMachine.AddEvent(launchEvent);
        this.SetLaunchHub(false);
        Debug.unityLogger.LogWarning("AddEvent", "Added a launch event to time machine.");

        // Submit event to online services.
        if (ApplicationState.isMultiplayer)
        {
            SubmitLaunchToServer(launchEvent);
        }
    }

    public async Task SubmitLaunchToServer(LaunchEvent launchEvent)
    {
        var client = ApplicationState.Client.getClient();

        var response = await client.GameEventClient.SubmitGameEvent(new SubmitGameEventRequest()
        {
            GameEventData = launchEvent.GetBaseGameEventModel().GameEventData,
        }, ApplicationState.currentGameConfig.Id);

        response.Get(
            (success) =>
            {
                    
            },
            (failure) =>
            {
                ApplicationState.CurrentGame.TimeMachine.RemoveEvent(launchEvent);
            }
        );
    }
    
    public async Task loadMultiplayerGame()
    {
        if (!ApplicationState.Client.isConnected)
        {
            // Show info here to try reconnect.
        }
        else
        {
            var gameEvents = await ApplicationState.Client.getClient().GameEventClient.GetGameRoomEvents(ApplicationState.currentGameConfig.Id);

            gameEvents.Get(
                (success) =>
                {
                    ApplicationState.CurrentGame.LoadGameEvents(success.GameEvents.ToList());
                
                    // go to current tick.
                    GameTick tick = new GameTick(ApplicationState.currentGameConfig.TimeStarted, DateTime.UtcNow);
                    ApplicationState.CurrentGame.TimeMachine.GoTo(tick);
                },
                (failure) =>
                {
                    // TODO: Tell the user that they are offline or an error occurred.
                }
            );
        }
    }

    public void loadSinglePlayerGame()
    {
        var players = new List<User>()
        {
            new User() { Id = "1", Username = "DefaultUser" },
            new User() { Id = "2", Username = "AnotherUser" },
        };
        
        GameConfiguration localConfig = new GameConfiguration()
        {
            Creator = players[0],
            ExpiresAt = DateTime.MaxValue,
            GameSettings = new GameSettings()
            {
                Goal = Goal.Domination,
                IsAnonymous = false,
                IsPrivate = false,
                IsRanked = false,
                MaxPlayers = 2,
                MinutesPerTick = 0.2
            },
            GameVersion = GameVersion.ALPHA01,
            Id = "Test",
            MapConfiguration = new MapConfiguration()
            {
                DormantsPerPlayer = 3,
                MaximumOutpostDistance = 200,
                MinimumOutpostDistance = 50,
                OutpostDistribution = new OutpostDistribution()
                {
                    FactoryWeight = 0.33f,
                    GeneratorWeight = 0.33f,
                    WatchtowerWeight = 0.33f
                },
                OutpostsPerPlayer = 5,
                Seed = 12345
            },
            PlayersInLobby = players,
            PlayerSpecialistDecks = new Dictionary<string, List<SpecialistTypeId>>()
            {
                { "1", new List<SpecialistTypeId>
                {
                    SpecialistTypeId.Advisor, SpecialistTypeId.Amnesiac, SpecialistTypeId.Assasin,
                    SpecialistTypeId.Automation, SpecialistTypeId.Bolster, SpecialistTypeId.Breeder, SpecialistTypeId.Dispatcher,
                    SpecialistTypeId.Economist, SpecialistTypeId.Enforcer, SpecialistTypeId.Engineer, SpecialistTypeId.Escort,
                    SpecialistTypeId.Foreman, SpecialistTypeId.Helmsman, SpecialistTypeId.Hypnotist, SpecialistTypeId.Icicle,
                    SpecialistTypeId.Industrialist ,SpecialistTypeId.Infiltrator, SpecialistTypeId.Inspector, SpecialistTypeId.Martyr,
                    SpecialistTypeId.Merchant, SpecialistTypeId.Pirate, SpecialistTypeId.Princess, SpecialistTypeId.Queen,
                    SpecialistTypeId.Saboteur, SpecialistTypeId.Sapper, SpecialistTypeId.Scrutineer, SpecialistTypeId.Sentry,
                    SpecialistTypeId.Smuggler, SpecialistTypeId.Sniper, SpecialistTypeId.Technician, SpecialistTypeId.Theif,
                    SpecialistTypeId.Tinkerer, SpecialistTypeId.Veteran, SpecialistTypeId.Warden, SpecialistTypeId.Double_Agent,
                    SpecialistTypeId.Intelligence_Officer, SpecialistTypeId.Iron_Maiden, SpecialistTypeId.Revered_Elder, SpecialistTypeId.SignalJammer
                } }, 
                { "2", new List<SpecialistTypeId>
                {
                    SpecialistTypeId.Advisor, SpecialistTypeId.Amnesiac, SpecialistTypeId.Assasin,
                    SpecialistTypeId.Automation, SpecialistTypeId.Bolster, SpecialistTypeId.Breeder, SpecialistTypeId.Dispatcher,
                    SpecialistTypeId.Economist, SpecialistTypeId.Enforcer, SpecialistTypeId.Engineer, SpecialistTypeId.Escort,
                    SpecialistTypeId.Foreman, SpecialistTypeId.Helmsman, SpecialistTypeId.Hypnotist, SpecialistTypeId.Icicle,
                    SpecialistTypeId.Industrialist ,SpecialistTypeId.Infiltrator, SpecialistTypeId.Inspector, SpecialistTypeId.Martyr,
                    SpecialistTypeId.Merchant, SpecialistTypeId.Pirate, SpecialistTypeId.Princess, SpecialistTypeId.Queen,
                    SpecialistTypeId.Saboteur, SpecialistTypeId.Sapper, SpecialistTypeId.Scrutineer, SpecialistTypeId.Sentry,
                    SpecialistTypeId.Smuggler, SpecialistTypeId.Sniper, SpecialistTypeId.Technician, SpecialistTypeId.Theif,
                    SpecialistTypeId.Tinkerer, SpecialistTypeId.Veteran, SpecialistTypeId.Warden, SpecialistTypeId.Double_Agent,
                    SpecialistTypeId.Intelligence_Officer, SpecialistTypeId.Iron_Maiden, SpecialistTypeId.Revered_Elder, SpecialistTypeId.SignalJammer
                } }
            },
            RoomName = "Sample Lobby",
            RoomStatus = RoomStatus.Ongoing,
        };
        Game game = new Game(localConfig);
        ApplicationState.CurrentGame = game;
        ApplicationState.currentGameConfig = localConfig;
        ApplicationState.player = new Player(players[0]);
    }

    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
