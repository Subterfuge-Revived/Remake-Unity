using System;
using System.Threading.Tasks;
using Grpc.Core;
using SubterfugeCore.Core.Players;
using SubterfugeRemakeService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rooms.Multiplayer
{
    public class NetworkClient
    {
        
        private SubterfugeClient.SubterfugeClient client = null;

        public bool isConnected { get; private set; } = false;

        public NetworkClient()
        {
            tryConnectServer();
        }

        public async Task<bool> tryConnectServer()
        {
            String Hostname = "52.14.116.178"; // For server
            // String Hostname = "localhost"; // For local
            int Port = 5000;

            client = new SubterfugeClient.SubterfugeClient(Hostname, Port.ToString());
        
            // Ensure that the client can connect to the server.
            try
            {
                await client.HealthCheckAsync(new HealthCheckRequest());
                return isConnected = true;
            }
            catch (RpcException exception)
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
                var response = await client.LoginWithTokenAsync(new AuthorizedTokenRequest() {
                    Token = token
                });

                if (response.Status.IsSuccess)
                {
                    ApplicationState.player = new Player(response.User.Id, response.User.Username);
                    return true;
                }
                PlayerPrefs.DeleteKey("token");
            }

            return false;
        }

        public SubterfugeClient.SubterfugeClient getClient()
        {
            return client;
        }
        
    }
}