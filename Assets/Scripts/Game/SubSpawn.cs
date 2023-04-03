using System.Collections.Generic;
using Subterfuge.Remake.Core.Entities;
using Subterfuge.Remake.Core.Entities.Components;
using Subterfuge.Remake.Core.Players;
using Subterfuge.Remake.Core.Timing;
using Subterfuge.Remake.Core.Topologies;
using UnityEngine;

public class SubSpawn : MonoBehaviour
{
    public Transform subPrefab;
    private Dictionary<Sub, List<GameObject>> spawnedSubs = new Dictionary<Sub, List<GameObject>>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (ApplicationState.CurrentGame == null)
            return;
            
        List<KeyValuePair<Sub, List<GameObject>>> orphans = new List<KeyValuePair<Sub, List<GameObject>>>();
        
        foreach (Sub sub in ApplicationState.CurrentGame.TimeMachine.GetState().GetSubList())
        {
            if (!spawnedSubs.ContainsKey(sub))
            {
                Debug.Log("Spawned new sub");
                InstantiateSub(sub);
            }
        }
        
        // Enumerate orphans to prevent concurrent modifications.
        foreach (KeyValuePair<Sub, List<GameObject>> kvpair in spawnedSubs)
        {
            if (!ApplicationState.CurrentGame.TimeMachine.GetState().EntityExists(kvpair.Key))
            {
                orphans.Add(kvpair);
            }
        }

        foreach (KeyValuePair<Sub, List<GameObject>> kvpair in orphans)
        {
            Debug.Log("Orphan detected, removing sub.");
            kvpair.Value.ForEach(Destroy);
            spawnedSubs.Remove(kvpair.Key);
        }
    }

    // TODO: Enable this.
    // The SubManager class updates the sub's position every tick and therefore needs to be able to know which quadrant the sub was initially spawned in to offplace the coordinates.
    // Disabling this for now until I can fix that...
    private void InstantiateSub(Sub sub)
    {
        /*
        // Spawn in all 9 quadrants for map wrapping.
        for (var vertical = 0; vertical < 3; vertical++)
        {
            for (var horizontal = 0; horizontal < 3; horizontal++)
            {
                Vector2 location = new Vector2(sub.GetComponent<PositionManager>().CurrentLocation.X, sub.GetComponent<PositionManager>().CurrentLocation.Y);
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
                Transform subObject = Instantiate(this.subPrefab, location, Quaternion.identity);
                subObject.GetComponent<SubManager>().sub = sub;

                if (!spawnedSubs.ContainsKey(sub))
                {
                    spawnedSubs[sub] = new List<GameObject>();
                }
                spawnedSubs[sub].Add(subObject.gameObject);
            }
        }
        */
        
        // Create a game object.
        Vector2 location = new Vector2(sub.GetComponent<PositionManager>().CurrentLocation.X, sub.GetComponent<PositionManager>().CurrentLocation.Y);
        Transform subObject = Instantiate(this.subPrefab, location, Quaternion.identity);
        subObject.GetComponent<SubManager>().sub = sub;

        if (!spawnedSubs.ContainsKey(sub))
        {
            spawnedSubs[sub] = new List<GameObject>();
        }
        spawnedSubs[sub].Add(subObject.gameObject);
    }
}
