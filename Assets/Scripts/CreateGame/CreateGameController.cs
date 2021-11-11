using System;
using SubterfugeCore.Core.Timing;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CreateGameController : MonoBehaviour
{
    

    public Slider playerCount;
    public Text gameTitle;
    public Toggle rankedToggle;
    public Toggle anonToggle;
    public Slider minutesPerTick;

    public async void onCreateGame()
    {
        var client = ApplicationState.Client.getClient();
        var seed = DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Second + DateTime.Now.Millisecond;
        var response = client.CreateNewRoom(new CreateRoomRequest(){
            GameSettings = new GameSettings()
            {
                Anonymous = anonToggle.isOn,
                Goal = Goal.Mining,
                IsRanked = rankedToggle.isOn,
                MaxPlayers = (int)playerCount.value,
                MinutesPerTick = minutesPerTick.value,
            },
            IsPrivate = false,
            MapConfiguration = new MapConfiguration()
            {
                DormantsPerPlayer = 3,
                MaximumOutpostDistance = 130,
                MinimumOutpostDistance = 30,
                OutpostDistribution = new OutpostWeighting()
                {
                    FactoryWeight = 0.40f,
                    GeneratorWeight = 0.40f,
                    WatchtowerWeight = 0.20f,
                },
                OutpostsPerPlayer = 4,
                Seed = new SeededRandom(seed).NextRand(0, 999999)
            },
            RoomName = gameTitle.text,
        });

        if (response.Status.IsSuccess)
        {
            SceneManager.LoadScene("GameSelect");
        }
        else
        {
            // TODO: Tell the user that their request failed. If error, show message, if timeout say offline.
            // If offline, potentially add their requests to a queue.
        }
    }

    public void showGeneralConfig()
    {
        
    }

    public void showMapConfig()
    {
        
    }

    public void showSpecialistConfig()
    {
        
    }

    public void onCancel()
    {
        SceneManager.LoadScene("GameSelect");
    }
    
    
}
