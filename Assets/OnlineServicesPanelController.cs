using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OnlineServicesPanelController : MonoBehaviour
{
    public Canvas registerPanel;
    public Canvas signInPanel;
    public Canvas LoggedOutPanel;
    public Canvas LoggedInPanel;
    public Canvas LoadingPanel;
    public Canvas NotConnectedPanel;
    
    // Start is called before the first frame update
    public async void Start()
    {
        registerPanel.gameObject.SetActive(false);
        signInPanel.gameObject.SetActive(false);
        LoggedInPanel.gameObject.SetActive(false);
        LoggedOutPanel.gameObject.SetActive(true);
        LoadingPanel.gameObject.SetActive(true);
        NotConnectedPanel.gameObject.SetActive(false);
        
        if (ApplicationState.Client.isConnected)
        {
            if (await ApplicationState.Client.IsPlayerLoggedIn())
            {
                showProfile();
            }
            else
            {
                showLoggedOutPanel();
            }
        }
        else
        {
            tryConnect();
            showDisconnected();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void tryConnect()
    {
        registerPanel.gameObject.SetActive(false);
        signInPanel.gameObject.SetActive(false);
        LoggedInPanel.gameObject.SetActive(false);
        LoggedOutPanel.gameObject.SetActive(false);
        LoadingPanel.gameObject.SetActive(true);
        NotConnectedPanel.gameObject.SetActive(false);

        if (await ApplicationState.Client.tryConnectServer())
        {
            if (await ApplicationState.Client.IsPlayerLoggedIn())
            {
                showProfile();
            }
            else
            {
                showLoggedOutPanel();
            }
        }
        else
        {
            showDisconnected();
        }
    }

    public void showLoggedOutPanel()
    {
        registerPanel.gameObject.SetActive(false);
        signInPanel.gameObject.SetActive(false);
        LoggedInPanel.gameObject.SetActive(false);
        LoggedOutPanel.gameObject.SetActive(true);
        LoadingPanel.gameObject.SetActive(false);
        NotConnectedPanel.gameObject.SetActive(false);
    }

    public void Back()
    {
        showLoggedOutPanel();
    }

    public void SignIn()
    {
        registerPanel.gameObject.SetActive(false);
        signInPanel.gameObject.SetActive(true);
        LoggedInPanel.gameObject.SetActive(false);
        LoggedOutPanel.gameObject.SetActive(false);
        LoadingPanel.gameObject.SetActive(false);
        NotConnectedPanel.gameObject.SetActive(false);
    }

    public void Register()
    {
        registerPanel.gameObject.SetActive(true);
        signInPanel.gameObject.SetActive(false);
        LoggedInPanel.gameObject.SetActive(false);
        LoggedOutPanel.gameObject.SetActive(false); 
        LoadingPanel.gameObject.SetActive(false);
        NotConnectedPanel.gameObject.SetActive(false);
    }

    public void showProfile()
    {
        registerPanel.gameObject.SetActive(false);
        signInPanel.gameObject.SetActive(false);
        LoggedInPanel.gameObject.SetActive(true);
        LoggedOutPanel.gameObject.SetActive(false);
        LoadingPanel.gameObject.SetActive(false);
        NotConnectedPanel.gameObject.SetActive(false);
    }

    public void showDisconnected()
    {
        registerPanel.gameObject.SetActive(false);
        signInPanel.gameObject.SetActive(false);
        LoggedInPanel.gameObject.SetActive(false);
        LoggedOutPanel.gameObject.SetActive(false);
        LoadingPanel.gameObject.SetActive(false);
        NotConnectedPanel.gameObject.SetActive(true);
    }
}
