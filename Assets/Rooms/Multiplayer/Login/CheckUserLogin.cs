using System;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Login
{
    public class CheckUserLogin : MonoBehaviour
    {
        public Text username;
        public Text password;

        public Text responseText;

        void Start()
        {
            responseText.text = "";
            
            // Check if user is logged in already.
            var token = PlayerPrefs.GetString("token");
            if (token != null)
            {
                var client = ApplicationState.Client.getClient();
                var response = client.LoginWithToken(new AuthorizedTokenRequest() {
                    Token = token
                });

                if (response.Status.IsSuccess)
                {
                    ApplicationState.player = new Player(response.User.Id, response.User.Username);
                    SceneManager.LoadScene("GameSelect");
                }
            }
            
        }
        
        public async void onLogin()
        {

            var client = ApplicationState.Client.getClient();
            var response = await client.LoginAsync(new AuthorizationRequest() {
                Username = username.text,
                Password = password.text,
            });
            
            if (response.Status.IsSuccess)
            {
                // Save the player
                ApplicationState.player = new Player(response.User.Id, response.User.Username);
                
                // Go to the main menu.
                PlayerPrefs.SetString("token", response.Token);
                SceneManager.LoadScene("GameSelect");
            }
            else
            {
                responseText.text = "Unable to login.";
            }
        }

        public void onCreateAccount()
        {
            SceneManager.LoadScene("CreateAccount");
        }
    }
}
