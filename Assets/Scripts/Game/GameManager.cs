using System;
using System.Linq;
using System.Threading.Tasks;
using SubterfugeCore.Core.Components;
using SubterfugeCore.Core.Entities.Positions;
using SubterfugeCore.Core.GameEvents.PlayerTriggeredEvents;
using SubterfugeCore.Core.Timing;
using SubterfugeCore.Models.GameEvents;
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

    public async Task launchSub()
    {
        var gameEventData = new GameEventData()
        {
            EventData = new LaunchEventData()
            {
                SourceId = launchOutpost.GetComponent<IdentityManager>().GetId(),
                DestinationId = destinationOutpost.GetComponent<IdentityManager>().GetId(),
                DrillerCount = (int)drillerSlider.value,
                SpecialistIds = { },
            },
            OccursAtTick = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick().GetTick()
        };
        
        LaunchEvent launchEvent = new LaunchEvent(
            new GameRoomEvent()
            {
                GameEventData = gameEventData
            });
        
        ApplicationState.CurrentGame.TimeMachine.AddEvent(launchEvent);
        this.SetLaunchHub(false);

        // Submit event to online services.
        if (ApplicationState.isMultiplayer)
        {
            var client = ApplicationState.Client.getClient();

            var response = await client.GameEventClient.SubmitGameEvent(new SubmitGameEventRequest()
            {
                GameEventData = gameEventData,
            }, ApplicationState.currentGameConfig.Id);

            if (!response.Status.IsSuccess)
            {
                ApplicationState.CurrentGame.TimeMachine.RemoveEvent(launchEvent);
                // Indicate an error, don't add to time machine.
            }
        }
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

            if (gameEvents.Status.IsSuccess)
            {
                ApplicationState.CurrentGame.LoadGameEvents(gameEvents.GameEvents.ToList());
            } else {
                // TODO: Tell the user that they are offline or an error occurred.
            }
                
            // go to current tick.
            GameTick tick = new GameTick(ApplicationState.currentGameConfig.TimeStarted, NtpConnector.GetNetworkTime());
            ApplicationState.CurrentGame.TimeMachine.GoTo(tick);
        }
    }

    public void loadSinglePlayerGame()
    {
            
    }

    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
