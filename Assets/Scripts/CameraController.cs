﻿using System.Collections;
using System.Collections.Generic;
 using SubterfugeCore.Core;
 using SubterfugeCore.Core.Entities.Locations;
 using SubterfugeCore.Core.GameEvents;
 using UnityEngine;
 using UnityEngine.EventSystems;

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
    
    // Track object being held/dragged
    private bool draggingMap = false;

    void OnStart()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the camera has a velocity, dampen the camera.
        if (rb.velocity.magnitude > 0)
        {
            if (rb.velocity.magnitude < 0.01) rb.velocity = new Vector2(0, 0);
            else rb.velocity -= rb.velocity * (Time.deltaTime * CameraDampen);
        }
        
        // If the pointer is not over the map AND if the initial touch/click point was not the map or outposts, return.
        if ((EventSystem.current.IsPointerOverGameObject()) && (!draggingMap)) return;

        // When the left mouse button is clicked, create a dragOrigin and velocity for the camera.
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(0, 0);
            // Firstly, check that the pressed location wasn't an outpost.
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null || !hit.collider.gameObject.CompareTag("Outpost"))
            {
                // The pressed location was not an outpost. Create a dragOrigin and velocity for the camera.
                Debug.Log("Left mouse down on map (not outpost).");
                dragOrigin = Input.mousePosition;
                draggingMap = true;
                return;
            }
        }
        
        // If the left mouse button is being held AND the initial touch/click point was on the map, pan the camera.
        if ((Input.GetMouseButton(0)) && (draggingMap))
        {
            if (dragOrigin == Input.mousePosition) return;

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(dragOrigin);

            dragOrigin = Input.mousePosition;
            transform.Translate(-pos, Space.World);
        }

        // If the left mouse button is released AND the initial touch/click was on the map, begin pan dampening.
        if ((Input.GetMouseButtonUp(0)) && (draggingMap))
        {
            rb.velocity = -((Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(dragOrigin)) / Time.deltaTime);
            draggingMap = false;
        }
    }
}
