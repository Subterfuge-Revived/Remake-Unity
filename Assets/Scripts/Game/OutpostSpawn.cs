using System.Collections.Generic;
using Subterfuge.Remake.Api.Network;
using Subterfuge.Remake.Core;
using Subterfuge.Remake.Core.Entities.Components;
using Subterfuge.Remake.Core.Entities.Positions;
using Subterfuge.Remake.Core.Players;
using Subterfuge.Remake.Core.Topologies;
using UnityEngine;

public class OutpostSpawn : MonoBehaviour
{
    public Transform outpostPrefab;
    public Transform outpostObject;
    private System.Random rand = new System.Random(0);
    List<Player> players = new List<Player>();

    private Dictionary<Outpost, List<GameObject>> spawnedOutposts = new Dictionary<Outpost, List<GameObject>>();


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        List<KeyValuePair<Outpost, List<GameObject>>> orphans = new List<KeyValuePair<Outpost, List<GameObject>>>();


        if (ApplicationState.CurrentGame == null)
            return;

        foreach (Outpost outpost in ApplicationState.CurrentGame.TimeMachine.GetState().GetOutposts())
        {
            if (!spawnedOutposts.ContainsKey(outpost))
            {
                Debug.Log("Spawned Outpost");
                InstantiateOutpost(outpost);
            }
        }

        // Enumerate orphans to prevent concurrent modifications.
        foreach (KeyValuePair<Outpost, List<GameObject>> kvpair in spawnedOutposts)
        {
            if (!ApplicationState.CurrentGame.TimeMachine.GetState().EntityExists(kvpair.Key))
            {
                orphans.Add(kvpair);
            }
        }

        foreach (KeyValuePair<Outpost, List<GameObject>> kvpair in orphans)
        {
            Debug.Log("Removing orphaned outpost.");
            kvpair.Value.ForEach(Destroy);
            spawnedOutposts.Remove(kvpair.Key);
        }
    }

    private void InstantiateOutpost(Outpost outpost)
    {
        Sprite sprite = getOutpostSprite(outpost);
        // Spawn in all 9 quadrants for map wrapping.
        for (var vertical = 0; vertical < 3; vertical++)
        {
            for (var horizontal = 0; horizontal < 3; horizontal++)
            {
                Vector2 location = new Vector2(outpost.GetComponent<PositionManager>().GetExpectedDestination().X,
                    outpost.GetComponent<PositionManager>().GetExpectedDestination().Y);
                if (vertical == 0)
                {
                    location.y -= RftVector.Map.Height;
                }

                if (vertical == 2)
                {
                    location.y += RftVector.Map.Height;
                }

                if (horizontal == 0)
                {
                    location.x -= RftVector.Map.Width;
                }

                if (horizontal == 2)
                {
                    location.x += RftVector.Map.Width;
                }

                // Create a game object.
                Transform outpostObject = Instantiate(outpostPrefab, location, Quaternion.identity);

                // Set the outpost sprite
                outpostObject.GetComponent<SpriteRenderer>().sprite = sprite;

                // Set the outpost and id in the manager.
                outpostObject.GetComponent<OutpostManager>().ID = outpost.GetComponent<IdentityManager>().GetId();
                outpostObject.GetComponent<OutpostManager>().outpost = outpost;
                
                if (!spawnedOutposts.ContainsKey(outpost))
                {
                    spawnedOutposts[outpost] = new List<GameObject>();
                }

                spawnedOutposts[outpost].Add(outpostObject.gameObject);
            }
        }
    }

    public Sprite getOutpostSprite(Outpost outpost)
    {
        Sprite sprite = Resources.Load<Sprite>("Locations/Unknown");;
        if (outpost.GetComponent<DrillerCarrier>().IsDestroyed())
        {
            sprite = Resources.Load<Sprite>("Locations/Destroyed");
        }
        else
        {
            switch (outpost.GetOutpostType())
            {
                case OutpostType.Generator:
                    sprite = Resources.Load<Sprite>("Locations/GeneratorFill");
                    break;
                case OutpostType.Factory:
                    sprite = Resources.Load<Sprite>("Locations/FactoryFill");
                    break;
                case OutpostType.Mine:
                    sprite = Resources.Load<Sprite>("Locations/MineFill");
                    break;
                case OutpostType.Watchtower:
                    sprite = Resources.Load<Sprite>("Locations/Watchtower");
                    break;
                default:
                    sprite = Resources.Load<Sprite>("Locations/Unknown");
                    break;
            }
        }

        return sprite;
    }
}
