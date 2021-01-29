using System;
using SubterfugeCore.Core.Network;
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

        public Text loginInfo;

        public async void onLogin()
        {
            // String Hostname = "server"; // For docker
            String Hostname = "localhost"; // For local
            int Port = 5000;

            var client = new SubterfugeClient.SubterfugeClient(Hostname, Port.ToString());
            Debug.Log("Sending login request");
            Debug.Log(username.text);
            Debug.Log(password.text);
            var response = await client.LoginAsync(new AuthorizationRequest() {
                Username = username.text,
                Password = password.text,
            });
            Debug.Log(response);
            if (response != null)
            {
                // Save the player
                ApplicationState.player = new Player(response.User.Id, response.User.Username);
                
                // Go to the main menu.
                PlayerPrefs.SetString("token", response.Token);
                SceneManager.LoadScene("GameSelect");
            }
            else
            {
                loginInfo.text = "Unable to login.";
            }
        }

        public void onCreateAccount()
        {
            SceneManager.LoadScene("CreateAccount");
        }
    }
}
