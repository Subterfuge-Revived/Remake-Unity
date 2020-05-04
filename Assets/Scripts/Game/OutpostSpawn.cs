using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Entities.Positions;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Network;
using SubterfugeCore.Core.Players;
using SubterfugeCore.Core.Topologies;

public class OutpostSpawn : MonoBehaviour
{
    public Transform generator;
    public Transform factory;
    public Transform mine;
    public Transform watchtower;
    public Transform destroyed;
    private Transform outpostObject;
    private System.Random rand = new System.Random(0);
    List<Player> players = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        /*
        // Get the gameroom from the applications state
        GameRoom room = ApplicationState.currentGameRoom;
        
        // Generate players
        foreach (NetworkUser networkUser in room.Players)
        {
            if (room.anonimity)
            {
                players.Add(new Player(networkUser.Id, "Player " + players.Count + 1)); 
            }
            else
            {
                players.Add(new Player(networkUser.Id, networkUser.Name));      
            }
        }

        //Build config
        GameConfiguration config = new GameConfiguration(players);
        config.seed = room.seed;
        config.dormantsPerPlayer = 3;
        config.maxiumumOutpostDistance = 100;
        config.minimumOutpostDistance = 30;
        config.outpostsPerPlayer = 5;
        */
        
        //////// GENERATE DUMMY PLAYERS ////////
        // (comment out above code)
        players.Add(new Player(1));
        players.Add(new Player(2));

        GameConfiguration config = new GameConfiguration(players);
        config.Seed = 1234;
        config.DormantsPerPlayer = 3;
        config.MaxiumumOutpostDistance = 100;
        config.MinimumOutpostDistance = 30;
        config.OutpostsPerPlayer = 5; 
        
        Game server = new Game(config);
        List<Outpost> outposts;
        List<Vector3> outpostLocations = new List<Vector3>();
        outposts = Game.TimeMachine.GetState().GetOutposts();

        foreach (Outpost outpost in outposts) {
            
            Vector2 location = new Vector2(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y);
            
            // Determine all quadrants to spawn in.
            Vector2 locationNorth = location;
            Vector2 locationSouth = location;
            Vector2 locationEast = location;
            Vector2 locationWest = location;
            Vector2 locationNorthWest = location;
            Vector2 locationNorthEast = location;
            Vector2 locationSouthWest = location;
            Vector2 locationSouthEast = location;
            
            
            locationNorth.y += RftVector.Map.Height;
            locationSouth.y -= RftVector.Map.Height;
            locationEast.x += RftVector.Map.Width;
            locationWest.x -= RftVector.Map.Width;

            locationNorthEast.y += RftVector.Map.Height;
            locationNorthEast.x += RftVector.Map.Width;
            
            locationNorthWest.y += RftVector.Map.Height;
            locationNorthWest.x -= RftVector.Map.Width;
            
            locationSouthEast.y -= RftVector.Map.Height;
            locationSouthEast.x += RftVector.Map.Width;
            
            locationSouthWest.y -= RftVector.Map.Height;
            locationSouthWest.x -= RftVector.Map.Width;

            Transform instanceType = null;
            
            
            /*
            Vector3 location = new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0);*/
            switch (outpost.GetOutpostType())
            {
                case OutpostType.Generator:
                    instanceType = generator;
                    break;
                case OutpostType.Factory:
                    instanceType = factory;
                    break;
                case OutpostType.Mine:
                    instanceType = mine;
                    break;
                case OutpostType.Watchtower:
                    instanceType = watchtower;
                    break;
                case OutpostType.Destroyed:
                    instanceType = destroyed;
                    break;
                default:
                    Debug.LogError("Could not spawn outpost");
                    break;
            }
            
            // Spawn all 9 regions.
            outpostObject = Instantiate(instanceType, location, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostLocations.Add(location);
            
            outpostObject = Instantiate(instanceType, locationNorth, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostObject = Instantiate(instanceType, locationSouth, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostObject = Instantiate(instanceType, locationEast, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostObject = Instantiate(instanceType, locationWest, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostObject = Instantiate(instanceType, locationNorthEast, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostObject = Instantiate(instanceType, locationNorthWest, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostObject = Instantiate(instanceType, locationSouthEast, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostObject = Instantiate(instanceType, locationSouthWest, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
        }

        Debug.Log("Spawned outposts");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
