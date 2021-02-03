using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        var response = client.CreateNewRoom(new CreateRoomRequest(){
            Anonymous = anonToggle.isOn,
            Goal = Goal.Mining,
            IsRanked = rankedToggle.isOn,
            MaxPlayers = (int)playerCount.value,
            MinutesPerTick = minutesPerTick.value,
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

    public void onCancel()
    {
        SceneManager.LoadScene("GameSelect");
    }
    
    
}
