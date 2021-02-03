using System;
using Grpc.Core;
using SubterfugeRemakeService;
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

        public bool tryConnectServer()
        {
            // String Hostname = "server"; // For docker
            String Hostname = "localhost"; // For local
            int Port = 5000;

            client = new SubterfugeClient.SubterfugeClient(Hostname, Port.ToString());
        
            // Ensure that the client can connect to the server.
            try
            {
                client.HealthCheck(new HealthCheckRequest());
                return isConnected = true;
            }
            catch (RpcException exception)
            {
                client = null;
                return isConnected = false;
            }
        }

        public SubterfugeClient.SubterfugeClient getClient()
        {
            return client;
        }
        
    }
}