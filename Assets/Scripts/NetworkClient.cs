using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Subterfuge.Remake.Api.Client;
using Subterfuge.Remake.Core.Players;
using UnityEngine;

namespace Rooms.Multiplayer
{
    public class NetworkClient
    {
        
        private SubterfugeClient client = null;

        public bool isConnected { get; private set; } = false;

        public NetworkClient()
        {
            tryConnectServer();
        }

        public T Get<T>(Func<int, T> respondWithType, Func<int, T> funcTwo)
        {
            return respondWithType(3);
        }

        public async Task<bool> tryConnectServer()
        {
            // String Hostname = "52.14.116.178"; // For server
            String Hostname = "http://localhost"; // For local
            int Port = 5295;

            client = new SubterfugeClient(Hostname + ":" + Port);

            // Ensure that the client can connect to the server.
            try
            {
                return (await client.HealthClient.Ping()).Get(
                    (success) =>
                    {
                        return true;
                    },
                    (failure) =>
                    {
                        client = null;
                        return false;
                    });
            }
            catch (SocketException exception)
            {
                isConnected = false;
                return false;
            }
        }

        public async Task<bool> IsPlayerLoggedIn(string token)
        {
            // Check if user is logged in already.
            if (token != null)
            {
                var client = ApplicationState.Client.getClient();
                client.UserApi.SetToken(token);
                var response = await client.HealthClient.AuthorizedPing();
                response.Get(
                    (success) =>
                    {
                        ApplicationState.player = new Player(success.LoggedInUser);
                    },
                    (failure) =>
                    {
                        Debug.LogError("Unable to login.");
                        PlayerPrefs.DeleteKey("token");
                    }
                );
            }

            return false;
        }

        public SubterfugeClient getClient()
        {
            return client;
        }
        
    }
}