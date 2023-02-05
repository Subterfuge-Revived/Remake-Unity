using SubterfugeCore.Core.Players;
using SubterfugeCore.Models.GameEvents;
using TMPro;
using UnityEngine;

namespace Login
{
    public class CheckUserLogin : MonoBehaviour
    {
        public TMP_InputField username;
        public TMP_InputField password;

        public TextMeshProUGUI responseText;
        public OnlineServicesPanelController menuPanelController;

        public async void onLogin()
        {
            var client = ApplicationState.Client.getClient();
            var response = await client.UserApi.Login(new AuthorizationRequest() {
                Username = username.text,
                Password = password.text,
            });
            
            if (response.Status.IsSuccess)
            {
                // Save the player
                ApplicationState.player = new Player(response.User.Id, response.User.Username);
                
                // Go to the main menu.
                PlayerPrefs.SetString("token", response.Token);
                menuPanelController.showLoggedInPanel();
            }
            else
            {
                responseText.text = "Unable to login, please try again.";
            }
        }
    }
}
