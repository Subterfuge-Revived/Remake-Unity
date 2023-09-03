using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core.Entities;
using Subterfuge.Remake.Core.Entities.Components;
using Subterfuge.Remake.Core.Entities.Positions;
using Subterfuge.Remake.Core.GameEvents.PlayerTriggeredEvents;
using Subterfuge.Remake.Core.Players;
using Subterfuge.Remake.Core.Timing;
using Subterfuge.Remake.Core.Topologies;
using UnityEngine;
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
    public Camera camera;
    
    // Start is called before the first frame update
    async void Start()
    {
        SetLaunchHub(false);
        // TODO: Add a configuration to the `Game` object to determine if it is a multiplayer game.
        if (ApplicationState.CurrentGame != null && ApplicationState.isMultiplayer)
        {
            loadMultiplayerGame();
        }
        
        // Set the default camera location to center on your player's outposts and set the zoom level to maximum of 1/2 the map size.
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -1);
    }
    
    private float GetDistanceToTouch(RftVector worldCoordinate, IEntity go)
    {
        return System.Numerics.Vector2.Distance(worldCoordinate.ToVector2(),
            go.GetComponent<PositionManager>().CurrentLocation.ToVector2());
    }

    private Outpost GetOutpostClosestToTouch(Touch touch)
    {
        // Set "z" to be 20 units away from the camera/UI layer to be able to raycast to the touch point.
        Vector3 touchVector = new Vector3(touch.position.x, touch.position.y, 20);

        // Raycast to the UI plane to determine the world coordinates from the touch.
        Vector3 worldPoint = camera.ScreenToWorldPoint(touchVector);
        RftVector worldCoordinate = new RftVector(worldPoint.x, worldPoint.y);

        List<Outpost> entitiesNearTouch = ApplicationState.CurrentGame.TimeMachine
            .GetState()
            .GetOutposts()
            .OrderBy(go => GetDistanceToTouch(worldCoordinate, go))
            .ToList();

        IEntity closestEntity = entitiesNearTouch.FirstOrDefault();
        float distanceToClosestEntity = GetDistanceToTouch(worldCoordinate, closestEntity);

        if (distanceToClosestEntity > 8.0f)
        {
            Debug.unityLogger.Log(
                $"No entities near touch. Closest: {closestEntity.GetComponent<IdentityManager>().GetName()} - {distanceToClosestEntity}rft");
            return null;
        }

        // Set the closest entity to touch.
        Outpost touchedOutpost = entitiesNearTouch.First() as Outpost;
        String sourceName = touchedOutpost.GetComponent<IdentityManager>().GetName();
        String sourceOwner = touchedOutpost.GetComponent<DrillerCarrier>().GetOwner()?.GetPlayerName();
        Debug.unityLogger.Log($"Touched {sourceName}({sourceOwner})");
        return touchedOutpost;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches.First();

            if (touch.phase == TouchPhase.Began)
            {
                Outpost touchedOutpost = GetOutpostClosestToTouch(touch);

                // Only set the launch outpost if the player owns it.
                if (Equals(touchedOutpost.GetComponent<DrillerCarrier>().GetOwner(), ApplicationState.player))
                {
                    launchOutpost = touchedOutpost;
                }
            }
            
            if (touch.phase == TouchPhase.Ended && launchOutpost != null)
            {
                Debug.unityLogger.Log("Touch ended with valid launch outpost...");
                destinationOutpost = GetOutpostClosestToTouch(touch);

                if (destinationOutpost == launchOutpost) return;
                String destinationName = destinationOutpost.GetComponent<IdentityManager>().GetName();
                String destinationOwner =
                    destinationOutpost.GetComponent<DrillerCarrier>().GetOwner()?.GetPlayerName();
                Debug.unityLogger.Log($"Destination: {destinationName}({destinationOwner})");

                SouceLaunchInformation sourcePanel = launchHud.GetComponentInChildren<SouceLaunchInformation>();
                sourcePanel.source = launchOutpost;
                SubLaunchInformation informationPanel = launchHud.GetComponentInChildren<SubLaunchInformation>();
                informationPanel.destination = destinationOutpost;
                informationPanel.sourceOutpost = launchOutpost;
                drillerSlider.maxValue = launchOutpost.GetComponent<DrillerCarrier>().GetDrillerCount();

                SetLaunchHub(true);
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

    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
