using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Entities.Locations;
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
        // Get the gameroom from the applications state
        GameRoom room = ApplicationState.currentGameRoom;
        
        // Generate players
        foreach (NetworkUser networkUser in room.players)
        {
            if (room.anonimity)
            {
                players.Add(new Player(networkUser.id, "Player " + players.Count + 1)); 
            }
            else
            {
                players.Add(new Player(networkUser.id, networkUser.name));      
            }
        }

        //Build config
        GameConfiguration config = new GameConfiguration(players);
        config.seed = room.seed;
        config.dormantsPerPlayer = 3;
        config.maxiumumOutpostDistance = 100;
        config.minimumOutpostDistance = 30;
        config.outpostsPerPlayer = 5;   
        
        Game server = new Game(config);
        List<Outpost> outposts;
        List<Vector3> outpostLocations = new List<Vector3>();
        outposts = Game.timeMachine.getState().getOutposts();

        foreach (Outpost outpost in outposts) {
            
            var location = new Vector3(rand.Next(-15,15), rand.Next(-15,15), 0);
            if (outpostLocations.Count > 0)
            {
                foreach (Vector3 OutpostLocation in outpostLocations)
                {
                    while (Vector3.Distance(location, OutpostLocation) < 2)
                    {
                        Debug.Log(Vector3.Distance(location, OutpostLocation) + " Relocating");
                        location = new Vector3(rand.Next(-15,15), rand.Next(-15,15), 0);
                    }
                }
            }
            /*
            Vector3 location = new Vector3(outpost.getCurrentLocation().X, outpost.getCurrentLocation().Y, 0);*/
            switch (outpost.getOutpostType())
            {
                case OutpostType.GENERATOR:
                    outpostObject = Instantiate(generator, location, Quaternion.identity); //new Vector3(outpost.getCurrentLocation().X, outpost.getCurrentLocation().Y, 0)
                    break;
                case OutpostType.FACTORY:
                    outpostObject = Instantiate(factory, location, Quaternion.identity); //new Vector3(outpost.getCurrentLocation().X, outpost.getCurrentLocation().Y, 0)
                    break;
                case OutpostType.MINE:
                    outpostObject = Instantiate(mine, location, Quaternion.identity); //new Vector3(outpost.getCurrentLocation().X, outpost.getCurrentLocation().Y, 0)
                    break;
                case OutpostType.WATCHTOWER:
                    outpostObject = Instantiate(watchtower, location, Quaternion.identity); //new Vector3(outpost.getCurrentLocation().X, outpost.getCurrentLocation().Y, 0)
                    break;
                case OutpostType.DESTROYED:
                    outpostObject = Instantiate(destroyed, location, Quaternion.identity); //new Vector3(outpost.getCurrentLocation().X, outpost.getCurrentLocation().Y, 0)
                    break;
                default:
                    Debug.LogError("Could not spawn outpost");
                    break;
            }

            outpostObject.GetComponent<OutpostManager>().ID = outpost.getGuid();
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
