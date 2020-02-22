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
        foreach (Sub sub in Game.timeMachine.getState().getSubList())
        {
            if (!subSpawned(sub))
            {
                Vector3 location = new Vector3(sub.getCurrentLocation().X, sub.getCurrentLocation().Y, 0);
                subObject = Instantiate(this.sub, location, Quaternion.identity);
                subObject.GetComponent<SubManager>().sub = sub;
                
                if (!spawnedSubs.ContainsKey(sub.getOwner()))
                {
                    spawnedSubs[sub.getOwner()] = new List<Sub>();
                }
                spawnedSubs[sub.getOwner()].Add(sub);
            }
        }
    }

    private bool subSpawned(Sub sub)
    {
        if (spawnedSubs.ContainsKey(sub.getOwner()))
        {
            foreach (Sub playerSub in spawnedSubs[sub.getOwner()])
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
