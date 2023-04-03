using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Rooms.Multiplayer.Loading;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core.Players;
using TMPro;
using Translation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{

    // Retry info
    private DateTime nextRetry = DateTime.Now.AddSeconds(-1);
    private int retryCount = 0;
    

    public Canvas loadingCanvas;
    public TextMeshProUGUI loadingText;
    public Canvas serverOfflineCanvas;
    public TextMeshProUGUI retryInText;

    public Canvas authRequiredCanvas;
    
    public Canvas loginCanvas;
    public Canvas registerAccountCanvas;

    // Start is called before the first frame update
    async void Start()
    {
        InitialState();
        // Load language strings.
        loadingText.text = "Loading Languages...";
        StringFactory.LoadStrings();
        loadingText.text = "Loading music...";
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioPlayer>().Play();
        loadingText.text = "Connecting to Server...";
        
        // Check if the server is online.
        await LoopUntilConnected();
        await AttemptLogin();
    }

    public void Update()
    {
        if (retryCount > 0)
        {
            var secondsUntilRetry = Math.Max(0, -1 * DateTime.Now.Subtract(nextRetry).Seconds);
            retryInText.text = "Retrying in " + secondsUntilRetry + ". Attempts: " + retryCount;
        }
    }
    
    private void InitialState()
    {
        loadingCanvas.gameObject.SetActive(true);
        serverOfflineCanvas.gameObject.SetActive(false);
        authRequiredCanvas.gameObject.SetActive(false);
    }

    private void ServerOffline()
    {
        loadingCanvas.gameObject.SetActive(true);
        serverOfflineCanvas.gameObject.SetActive(true);
        authRequiredCanvas.gameObject.SetActive(false);
    }

    public async Task LoopUntilConnected()
    {
        var connected = false;
        while (!connected)
        {
            try
            {
                connected = await ApplicationState.Client.tryConnectServer();
            }
            catch (Exception exception)
            {
                connected = false;
            }

            if (!connected)
            {
                ServerOffline();
                retryCount++;
                var delaySeconds = getRetryDelay();
                nextRetry = DateTime.Now.AddSeconds(delaySeconds);
                await Task.Delay(delaySeconds * 1000);
            }
        }
    }

    public async Task<bool> AttemptLogin()
    {
        loadingText.text = "Authenticating...";
        var isLoggedIn = await ApplicationState.Client.IsPlayerLoggedIn(PlayerPrefs.GetString("token"));

        
        if (isLoggedIn)
        {
            SceneManager.LoadScene("MainMenu");
            return true;
        }
        else
        {
            PlayerPrefs.DeleteKey("token");
            // Show Login and registration
            loadingCanvas.gameObject.SetActive(false);
            serverOfflineCanvas.gameObject.SetActive(false);
            authRequiredCanvas.gameObject.SetActive(true);
            loginCanvas.gameObject.SetActive(true);
            registerAccountCanvas.gameObject.SetActive(false);
            return false;
        }
    }

    private int getRetryDelay()
    {
        return Math.Min(30, 5 * retryCount);
    }

    public void ShowLoginCanvas()
    {
        loginCanvas.gameObject.SetActive(true);
        registerAccountCanvas.gameObject.SetActive(false);
    }

    public void ShowRegisterCanvas()
    {
        loginCanvas.gameObject.SetActive(false);
        registerAccountCanvas.gameObject.SetActive(true);
    }

    public async void UserClickLogin()
    {
        LoginData loginData = loginCanvas.GetComponent<LoginData>();

        var loginResponse = await ApplicationState.Client.getClient().UserApi.Login(new AuthorizationRequest()
        {
            Password = loginData.Password.text,
            Username = loginData.Username.text,
        });

        loginResponse.Get(
            (success) =>
            {
                PlayerPrefs.SetString("token", success.Token);
                ApplicationState.player = new Player(success.User);
                SceneManager.LoadScene("MainMenu");
            },
            (failure) =>
            {
                Debug.LogError("Unable to login. Please try again.");
            }
        );
    }

    public async void UserClickRegister()
    {
        RegisterData registerData = registerAccountCanvas.GetComponent<RegisterData>();

        if (!registerData.Validate())
        {
            // Do something?
        }

        var registerResponse = await ApplicationState.Client.getClient().UserApi.RegisterAccount(new AccountRegistrationRequest()
        {
            Password = registerData.Password.text,
            Username = registerData.Username.text,
            // DeviceIdentifier = SystemInfo.deviceUniqueIdentifier,
            DeviceIdentifier = Guid.NewGuid().ToString(),
            PhoneNumber = registerData.Phone.text,
        });

        registerResponse.Get(
            (success) =>
            {
                PlayerPrefs.SetString("token", success.Token);
                ApplicationState.player = new Player(success.User);
                SceneManager.LoadScene("MainMenu");
            },
            (failure) =>
            {

            }
        );
    }
}
