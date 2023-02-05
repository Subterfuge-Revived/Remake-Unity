using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Components;
using SubterfugeCore.Core.Entities;
using SubterfugeCore.Core.Timing;
using TMPro;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class SubManager : MonoBehaviour
{
    public Sub sub;
    private TextMeshPro textMesh;

    // Start is called before the first frame update
    void Start()
    {
        GameTick currentTick = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick();
        PositionManager pm = sub.GetComponent<PositionManager>();
        var currentPosition = pm.GetPositionAt(currentTick);
        var drillerCarrier = sub.GetComponent<DrillerCarrier>();
        var owner = drillerCarrier.GetOwner();
        
        // Update the position and rotation of the sub.
        Vector3 location = new Vector3(currentPosition.X, currentPosition.Y, 0);
        Transform transform = GetComponent<Transform>();
        transform.localPosition = location;
        
        int rotationAngle = (int) (pm.GetRotationRadians() * (360 / (2 * Math.PI)) - 90);
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        
        // Set color based on the owner
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int playerId = 0;
        if (owner != null)
        {
            playerId = ApplicationState.CurrentGame.TimeMachine.GetState().GetPlayers().IndexOf(owner) + 1;
        }
        switch (playerId)
        {
            case 0:
                renderer.color = Color.white;
                break;
            case 1:
                renderer.color = Color.red;
                break;
            case 2:
                renderer.color = Color.blue;
                break;
            case 3:
                renderer.color = Color.green;
                break;
            case 4:
                renderer.color = Color.cyan;
                break;
            case 5:
                renderer.color = Color.magenta;
                break;
            case 6:
                renderer.color = Color.yellow;
                break;
            case 7:
                renderer.color = Color.black;
                break;
        }
        
        
        textMesh = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        GameTick currentTick = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick();
        var drillerCarrier = sub.GetComponent<DrillerCarrier>();
        PositionManager pm = sub.GetComponent<PositionManager>();
        var currentPosition = pm.GetPositionAt(currentTick);
        
        
        textMesh.text = drillerCarrier.GetDrillerCount().ToString();
        if (ApplicationState.CurrentGame.TimeMachine.GetState().SubExists(sub))
        {
            // Update the position and rotation of the sub.
            Vector3 location = new Vector3(currentPosition.X, currentPosition.Y, 0);
            Transform transform = GetComponent<Transform>();
            transform.localPosition = location;
            
            int rotationAngle = (int) (pm.GetRotationRadians() * (360 / (2 * Math.PI)) - 90);
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
        else
        {
            /*
            if ( PrefabUtility.IsPartOfPrefabInstance(transform) )
            {
                //if a part of a prefab instance then get the instance handle
                Object prefabInstance = PrefabUtility.GetPrefabInstanceHandle(transform);
                //destroy the handle
                GameObject.DestroyImmediate(prefabInstance);
            }
            */
            //the usual destroy immediate to clean up scene objects
            GameObject.DestroyImmediate(transform.gameObject,true);
        }
    }
}
