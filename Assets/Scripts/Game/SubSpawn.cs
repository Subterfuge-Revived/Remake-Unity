using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Entities;
using SubterfugeCore.Core.Players;
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
        foreach (Sub sub in Game.TimeMachine.GetState().GetSubList())
        {
            if (!subSpawned(sub))
            {
                Vector3 location = new Vector3(sub.GetCurrentPosition().X, sub.GetCurrentPosition().Y, 0);
                subObject = Instantiate(this.sub, location, Quaternion.identity);
                subObject.GetComponent<SubManager>().sub = sub;
                
                if (!spawnedSubs.ContainsKey(sub.GetOwner()))
                {
                    spawnedSubs[sub.GetOwner()] = new List<Sub>();
                }
                spawnedSubs[sub.GetOwner()].Add(sub);
            }
        }
        
        // Get all references to destroyed subs.
        List<Sub> orphans = new List<Sub>();
        
        foreach(Player p in spawnedSubs.Keys)
        {
            // Check the subs exist.
            foreach (Sub s in spawnedSubs[p])
            {
                if (!Game.TimeMachine.GetState().SubExists(s))
                {
                    // Remove the sub from the array.
                    orphans.Add(s);
                }   
            }
        }
        
        // Cleanup orphaned subs
        foreach (Sub s in orphans)
        {
            spawnedSubs[s.GetOwner()].Remove(s);
        }
    }

    private bool subSpawned(Sub sub)
    {
        if (spawnedSubs.ContainsKey(sub.GetOwner()))
        {
            foreach (Sub playerSub in spawnedSubs[sub.GetOwner()])
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
