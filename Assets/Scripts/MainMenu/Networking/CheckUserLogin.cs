using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core.Players;
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

            response.Get(
                (success) =>
                {
                    // Save the player
                    ApplicationState.player = new Player(success.User);
                
                    // Go to the main menu.
                    PlayerPrefs.SetString("token", success.Token);
                    menuPanelController.showLoggedInPanel();
                },
                (failure) =>
                {
                    responseText.text = "Unable to login, please try again.";
                }
            );
        }
    }
}
