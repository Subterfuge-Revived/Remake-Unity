using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Entities;
using UnityEngine;

public class SubManager : MonoBehaviour
{
    public Sub sub;
    // Start is called before the first frame update
    void Start()
    {
        // Update the position and rotation of the sub.
        Vector3 location = new Vector3(sub.getCurrentLocation().X, sub.getCurrentLocation().Y, 0);
        Transform transform = GetComponent<Transform>();
        transform.localPosition = location;
        transform.rotation = Quaternion.Euler(0, 0, (int)(sub.getRotation() * (360 / (2 * Math.PI))) - 135);
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.timeMachine.getState().subExists(sub))
        {
            // Update the position and rotation of the sub.
            Vector3 location = new Vector3(sub.getCurrentLocation().X, sub.getCurrentLocation().Y, 0);
            Transform transform = GetComponent<Transform>();
            transform.localPosition = location;
            transform.rotation = Quaternion.Euler(0, 0, (int) (sub.getRotation() * (360 / (2 * Math.PI))) - 135);
        }
        else
        {
            Destroy(this);
        }
    }
}
