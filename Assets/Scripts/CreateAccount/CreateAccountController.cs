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
    public Api api;
    public Text responseInfo;
    
    public void Start()
    {
        api = gameObject.GetComponent<Api>();
    }

    public async void onRegister()
    {
        RegisterResponse response = await api.registerAccount(username.text, password.text, email.text);
        if (response.success)
        {
            ApplicationState.player = new Player(response.user.id, response.user.name);
            PlayerPrefs.SetString("username", username.text);
            PlayerPrefs.SetString("password", password.text);
            PlayerPrefs.SetString("token", response.token);
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            responseInfo.text = response.message;
        }
    }

    public void onCancel()
    {
        SceneManager.LoadScene("LoginScreen");
    }
}
