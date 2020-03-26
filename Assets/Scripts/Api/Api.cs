using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SubterfugeCore;
using SubterfugeCore.Core.Network;
using SubterfugeCore.Core.Players;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Api : MonoBehaviour
{
    
    static readonly HttpClient client = new HttpClient();
    private string url = "http://localhost/subterfuge-backend/sandbox/event_exec.php";
    private string SESSION_ID = null;
    public bool isAuthenticated { get; private set; }

    public async void TryLogin()
    {
        InputField[] inputFields = FindObjectsOfType<InputField>();

        string password = "";
        string username = "";
        
        // Get form inputs
        foreach (InputField field in inputFields)
        {
            if (field.name == "PasswordInput")
            {
                password = field.text;
            }
            
            if (field.name == "UsernameInput")
            {
                username = field.text;
            }
        }
        
        LoginResponse loginResponse = await this.Login(username, password);

        if (loginResponse.success)
        {
            // Save player login information to their device so they don't need to sign in again.
            ApplicationState.player = new Player(loginResponse.user);
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.SetString("password", password);
            PlayerPrefs.SetString("token", loginResponse.token);
            isAuthenticated = true;
            SESSION_ID = loginResponse.token;
            
            // Go to the main menu.
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            // show an error message about the login.
            Text[] textFields = FindObjectsOfType<Text>();
            // Get form inputs
            foreach (Text text in textFields)
            {
                if (text.name == "LoginInfo")
                {
                    text.text = loginResponse.message;
                }
            }
        }
    }
    
    public async Task<LoginResponse> Login(string username, string password)
    {
        try
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("player_name", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("type", "login")
            });

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log(responseContent);

            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
            return loginResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<List<GameRoom>> GetOpenRooms()
    {
        try
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("session_id", PlayerPrefs.GetString("token")),
                new KeyValuePair<string, string>("type", "get_room_data"),
                new KeyValuePair<string, string>("room_status", "open")
            });

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log(responseContent);

            List<GameRoom> roomListResponse = JsonConvert.DeserializeObject<List<GameRoom>>(responseContent);
            return roomListResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public async Task<List<GameRoom>> GetOngoingRooms()
    {
        try
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("session_id", PlayerPrefs.GetString("token")),
                new KeyValuePair<string, string>("type", "get_room_data"),
                new KeyValuePair<string, string>("room_status", "ongoing"),
                new KeyValuePair<string, string>("filter_player", "true")
            });

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log(responseContent);

            List<GameRoom> roomListResponse = JsonConvert.DeserializeObject<List<GameRoom>>(responseContent);
            return roomListResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }


    public async Task<JoinLobbyResponse> JoinLobby(int roomId)
    {
        try
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("session_id", PlayerPrefs.GetString("token")),
                new KeyValuePair<string, string>("type", "join_room"),
                new KeyValuePair<string, string>("room_id", roomId.ToString()),
            });

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log(responseContent);

            JoinLobbyResponse roomListResponse = JsonConvert.DeserializeObject<JoinLobbyResponse>(responseContent);
            return roomListResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public async Task<LeaveLobbyResponse> LeaveLobby(int roomId)
    {
        try
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("session_id", PlayerPrefs.GetString("token")),
                new KeyValuePair<string, string>("type", "leave_room"),
                new KeyValuePair<string, string>("room_id", roomId.ToString()),
            });

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log(responseContent);

            LeaveLobbyResponse roomListResponse = JsonConvert.DeserializeObject<LeaveLobbyResponse>(responseContent);
            return roomListResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    public async Task<StartLobbyEarlyResponse> StartLobbyEarly(int roomId)
    {
        try
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("session_id", PlayerPrefs.GetString("token")),
                new KeyValuePair<string, string>("type", "start_early"),
                new KeyValuePair<string, string>("room_id", roomId.ToString()),
            });

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log(responseContent);

            StartLobbyEarlyResponse roomListResponse = JsonConvert.DeserializeObject<StartLobbyEarlyResponse>(responseContent);
            return roomListResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public async Task<CreateLobbyResponse> CreateLobby(string title, int max_players, int min_rating, bool rated, bool anonymous, int goal, int map)
    {
        try
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("session_id", PlayerPrefs.GetString("token")),
                new KeyValuePair<string, string>("type", "new_room"),
                new KeyValuePair<string, string>("description", title),
                new KeyValuePair<string, string>("max_players", max_players.ToString()),
                new KeyValuePair<string, string>("min_rating", min_rating.ToString()),
                new KeyValuePair<string, string>("rated", rated.ToString()),
                new KeyValuePair<string, string>("anonymity", anonymous.ToString()),
                new KeyValuePair<string, string>("goal", goal.ToString()),
                new KeyValuePair<string, string>("map", map.ToString()),
            });

            HttpResponseMessage response = await client.PostAsync(url, formContent);
            // Read the response
            string responseContent = await response.Content.ReadAsStringAsync();
            Debug.Log(responseContent);

            CreateLobbyResponse roomListResponse = JsonConvert.DeserializeObject<CreateLobbyResponse>(responseContent);
            return roomListResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
}
