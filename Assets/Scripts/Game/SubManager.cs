﻿using System;
using Subterfuge.Remake.Core.Entities;
using Subterfuge.Remake.Core.Entities.Components;
using Subterfuge.Remake.Core.Timing;
using TMPro;
using UnityEngine;

public class SubManager : MonoBehaviour
{
    public Sub sub;
    private TextMeshPro textMesh;
    private LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        GameTick currentTick = ApplicationState.CurrentGame.TimeMachine.GetCurrentTick();
        PositionManager pm = sub.GetComponent<PositionManager>();
        var currentPosition = pm.CurrentLocation;
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
        var currentPosition = pm.CurrentLocation;
        
        _lineRenderer.SetPosition(0, new Vector3(pm.CurrentLocation.X, pm.CurrentLocation.Y, 1));
        _lineRenderer.SetPosition(1, new Vector3(pm.GetExpectedDestination().X, pm.GetExpectedDestination().Y, 1));
        
        
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
    }
}
