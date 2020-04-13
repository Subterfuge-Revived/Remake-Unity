using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using SubterfugeCore.Core.Timing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RulerSlider : MonoBehaviour, IDragHandler
{
    private int tickNumber = 0;
    private float currentPosition = 0;
    public Text CurrentTick;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tickNumber = (int)Math.Floor(currentPosition / -6);
        CurrentTick.text = tickNumber.ToString();
        if (tickNumber >= 0)
        {
            Game.TimeMachine.GoTo(GameTick.FromTickNumber(tickNumber));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        Vector2 movement = eventData.delta;
        movement.x /= 4;
        movement.y = 0;
        // Get all child components and transform them
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position += (Vector3)movement;
        }

        currentPosition += movement.x;
    }
}
