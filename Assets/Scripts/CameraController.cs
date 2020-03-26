﻿using System.Collections;
using System.Collections.Generic;
 using SubterfugeCore.Core;
 using SubterfugeCore.Core.Entities.Locations;
 using SubterfugeCore.Core.GameEvents;
 using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float CameraDampen = 5;
    private Vector3 dragOrigin;
    private float speed;
    private RaycastHit2D hit;
    private GameManager gameManager;
    
    // Variables for tracking sub launches.
    private Outpost launchOutpost = null;
    private Outpost destinationOutpost = null;

    void OnStart()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If left mouse button is down, the camera is being moved. Set the drag origin and create a velocity for the camera
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the pressed location was an outpost. If it was, the user is trying to launch a sub.
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "Outpost")
            {
                // Clicked object is an outpost, don't move the camera.
                launchOutpost = hit.collider.gameObject.GetComponent<OutpostManager>().outpost;
                dragOrigin = Input.mousePosition;
                Debug.Log("Initially clicked an outpost!");
                return;
            }
            else
            {
                Debug.Log("Reset outpost click.");
                launchOutpost = null;
            }
            
            rb.velocity = new Vector2(0,0);
            dragOrigin = Input.mousePosition;
            return;   
        }

        // If the mouse button is released, apply velocity to the map to scroll
        if (Input.GetMouseButtonUp(0))
        {
            // If the first click was on an outpost, check if the second is on another outpost for a launch.
            if (launchOutpost != null)
            {
                // Check if the pressed location was an outpost. If it was, the user is trying to launch a sub.
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.tag == "Outpost")
                {
                    // Clicked object is an outpost, don't move the camera.
                    destinationOutpost = hit.collider.gameObject.GetComponent<OutpostManager>().outpost;
                    
                    // Launch a sub
                    LaunchEvent launchEvent = new LaunchEvent(Game.timeMachine.getCurrentTick().getNextTick(), launchOutpost, 1, destinationOutpost);
                    Game.timeMachine.addEvent(launchEvent);
                    Debug.Log("Launched a sub!");
                    launchOutpost = null;
                    destinationOutpost = null;
                    dragOrigin = Input.mousePosition;
                }
            }
            // The first click was not on an outpost, apply velocity to the map to scroll.
            else
            {
                rb.velocity = -((Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(dragOrigin)) / Time.deltaTime);
            }
        }

        // If the mouse is not pressed, slow the map down over time to a stop
        if (!Input.GetMouseButton(0))
        {
            if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
            {
                if ((Mathf.Abs(rb.velocity.x) > 0 && Mathf.Abs(rb.velocity.x) < 0.01) || (Mathf.Abs(rb.velocity.y) > 0 && Mathf.Abs(rb.velocity.y) < 0.0))
                {
                    rb.velocity = new Vector2(0, 0);
                }
                else
                {
                    rb.velocity += new Vector2(-rb.velocity.x * Time.deltaTime * CameraDampen, -rb.velocity.y * Time.deltaTime * CameraDampen); //Slow camera down
                }
            }
            return;
        }
        if (dragOrigin == Input.mousePosition) return;
        if (launchOutpost != null) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(dragOrigin);

        dragOrigin = Input.mousePosition;
        transform.Translate(-pos, Space.World);
    }
}
