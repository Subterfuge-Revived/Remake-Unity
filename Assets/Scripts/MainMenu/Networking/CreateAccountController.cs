using System;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core.Players;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateAccountController : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField phoneNumber;
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
            Password = password.text,
            Username = username.text,
            PhoneNumber = phoneNumber.text,
        });

        response.Get(
            (success) =>
            {
                ApplicationState.player = new Player(success.User);
                PlayerPrefs.SetString("username", username.text);
                PlayerPrefs.SetString("password", password.text);
                PlayerPrefs.SetString("token", success.Token);
                menuPanelController.showLoggedInPanel();
            },
            (failure) =>
            {
                responseInfo.text = failure.Detail;
            }
        );
    }
}
