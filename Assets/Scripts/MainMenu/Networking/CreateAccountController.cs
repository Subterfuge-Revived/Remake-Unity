using System;
using SubterfugeCore.Core.Players;
using SubterfugeCore.Models.GameEvents;
using TMPro;
using UnityEngine;

public class CreateAccountController : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField email;
    public TextMeshProUGUI responseInfo;
    public OnlineServicesPanelController menuPanelController;
    
    public void Start()
    {
        responseInfo.text = "";
    }

    public async void onRegister()
    {
        var client = ApplicationState.Client.getClient();
        var response = await client.UserApi.RegisterAccount(new AccountRegistrationRequest()
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
            menuPanelController.showLoggedInPanel();
        }
        else
        {
            responseInfo.text = response.Status.Detail;
        }
    }
}
