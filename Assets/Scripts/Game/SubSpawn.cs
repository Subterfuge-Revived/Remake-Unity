using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Components;
using SubterfugeCore.Core.Entities;
using SubterfugeCore.Core.Players;
using SubterfugeCore.Core.Timing;
using UnityEngine;
using UnityEngine.Animations;

public class SubSpawn : MonoBehaviour
{
    public Transform sub;
    private Transform subObject;
    Dictionary<Player, List<Sub>> spawnedSubs = new Dictionary<Player, List<Sub>>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameTick currentTick = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick();
        foreach (Sub sub in ApplicationState.CurrentGame.TimeMachine.GetState().GetSubList())
        {
            if (!subSpawned(sub))
            {
                Vector3 location = new Vector3(sub.GetComponent<PositionManager>().GetPositionAt(currentTick).X, sub.GetComponent<PositionManager>().GetPositionAt(currentTick).Y, 0);
                subObject = Instantiate(this.sub, location, Quaternion.identity);
                subObject.GetComponent<SubManager>().sub = sub;
                
                var owner = sub.GetComponent<DrillerCarrier>().GetOwner();
                if (!spawnedSubs.ContainsKey(owner))
                {
                    spawnedSubs[owner] = new List<Sub>();
                }
                spawnedSubs[owner].Add(sub);
            }
        }
        
        // Get all references to destroyed subs.
        List<Sub> orphans = new List<Sub>();
        
        foreach(Player p in spawnedSubs.Keys)
        {
            // Check the subs exist.
            foreach (Sub s in spawnedSubs[p])
            {
                if (!ApplicationState.CurrentGame.TimeMachine.GetState().SubExists(s))
                {
                    // Remove the sub from the array.
                    orphans.Add(s);
                }   
            }
        }
        
        // Cleanup orphaned subs
        foreach (Sub s in orphans)
        {
            spawnedSubs[s.GetComponent<DrillerCarrier>().GetOwner()].Remove(s);
        }
    }

    private bool subSpawned(Sub sub)
    {
        if (spawnedSubs.ContainsKey(sub.GetComponent<DrillerCarrier>().GetOwner()))
        {
            foreach (Sub playerSub in spawnedSubs[sub.GetComponent<DrillerCarrier>().GetOwner()])
            {
                if (playerSub.Equals(sub))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
