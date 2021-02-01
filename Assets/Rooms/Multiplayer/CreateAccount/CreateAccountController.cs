using System;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAccountController : MonoBehaviour
{
    public Text username;
    public Text password;
    public Text email;
    public Text responseInfo;
    
    public void Start()
    {
        responseInfo.text = "";
    }

    public async void onRegister()
    {
        var client = ApplicationState.Client.getClient();
        var response = client.RegisterAccount(new AccountRegistrationRequest()
        {
            // DeviceIdentifier = SystemInfo.deviceUniqueIdentifier,
            DeviceIdentifier = Guid.NewGuid().ToString(),
            Email = email.text,
            Password = password.text,
            Username = username.text,
        });
            
        if (response.Status.IsSuccess)
        {
            ApplicationState.player = new Player(response.User.Id, response.User.Username);
            PlayerPrefs.SetString("username", username.text);
            PlayerPrefs.SetString("password", password.text);
            PlayerPrefs.SetString("token", response.Token);
            SceneManager.LoadScene("GameSelect");
        }
        else
        {
            responseInfo.text = response.Status.Detail;
        }
    }

    public void onCancel()
    {
        SceneManager.LoadScene("LoginScreen");
    }
}
