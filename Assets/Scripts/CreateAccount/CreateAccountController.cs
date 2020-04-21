using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SubterfugeCore.Core.Network;
using SubterfugeCore.Core.Players;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAccountController : MonoBehaviour
{
    public Text username;
    public Text password;
    public Text email;
    Api api = new Api("http://18.220.154.6/api");
    public Text responseInfo;
    
    public void Start()
    {
    }

    public async void onRegister()
    {
        RegisterResponse response = await api.RegisterAccount(username.text, password.text, email.text);
        if (response.Success)
        {
            ApplicationState.player = new Player(response.User.Id, response.User.Name);
            PlayerPrefs.SetString("username", username.text);
            PlayerPrefs.SetString("password", password.text);
            PlayerPrefs.SetString("token", response.Token);
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            responseInfo.text = response.Message;
        }
    }

    public void onCancel()
    {
        SceneManager.LoadScene("LoginScreen");
    }
}
