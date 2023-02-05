using System;
using System.Threading.Tasks;
using Grpc.Core;
using SubterfugeCore.Core.Players;
using SubterfugeRestApiClient;
using SubterfugeRestApiClient.controllers.exception;
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

        public async Task<bool> tryConnectServer()
        {
            // String Hostname = "52.14.116.178"; // For server
            String Hostname = "localhost"; // For local
            int Port = 5000;

            client = new SubterfugeClient(Hostname + ":" + Port);
        
            // Ensure that the client can connect to the server.
            try
            {
                await client.HealthClient.Ping();
                return isConnected = true;
            }
            catch (SubterfugeClientException exception)
            {
                client = null;
                return isConnected = false;
            }
        }

        public async Task<bool> IsPlayerLoggedIn()
        {
            // Check if user is logged in already.
            var token = PlayerPrefs.GetString("token");
            if (token != null)
            {
                var client = ApplicationState.Client.getClient();
                try
                {
                    client.UserApi.SetToken(token);
                    var response = await client.HealthClient.AuthorizedPing();
                    if (response.Status.IsSuccess)
                    {
                        ApplicationState.player = new Player(response.LoggedInUser.Id, response.LoggedInUser.Username);
                        return true;
                    }
                    PlayerPrefs.DeleteKey("token");
                } catch (RpcException exception)
                {
                    return false;
                }
            }

            return false;
        }

        public SubterfugeClient getClient()
        {
            return client;
        }
        
    }
}