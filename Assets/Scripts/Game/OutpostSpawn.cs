using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Entities.Positions;
using SubterfugeCore.Core.Generation;
using SubterfugeCore.Core.Network;
using SubterfugeCore.Core.Players;

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
            /*
            Vector3 location = new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0);*/
            switch (outpost.GetOutpostType())
            {
                case OutpostType.Generator:
                    outpostObject = Instantiate(generator, location, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
                    break;
                case OutpostType.Factory:
                    outpostObject = Instantiate(factory, location, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
                    break;
                case OutpostType.Mine:
                    outpostObject = Instantiate(mine, location, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
                    break;
                case OutpostType.Watchtower:
                    outpostObject = Instantiate(watchtower, location, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
                    break;
                case OutpostType.Destroyed:
                    outpostObject = Instantiate(destroyed, location, Quaternion.identity); //new Vector3(outpost.GetCurrentPosition().X, outpost.GetCurrentPosition().Y, 0)
                    break;
                default:
                    Debug.LogError("Could not spawn outpost");
                    break;
            }

            outpostObject.GetComponent<OutpostManager>().ID = outpost.GetId();
            // Set a reference to the original outpost
            outpostObject.GetComponent<OutpostManager>().outpost = outpost;
            outpostLocations.Add(location);
        }

        Debug.Log("Spawned outposts");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
