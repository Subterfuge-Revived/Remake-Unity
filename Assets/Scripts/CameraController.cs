﻿using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Entities.Locations;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float CameraDampen = 5;
    private Vector3 dragOrigin;
    private float speed;
    private RaycastHit2D hit;
    private GameManager gameManager;

    void OnStart()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(0,0);
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = -((Input.mousePosition - dragOrigin) / Time.deltaTime) * 0.01f;
        }

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

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

        dragOrigin = Input.mousePosition;
        Vector3 move = new Vector3(-pos.x*10, -pos.y*10, 0);
        transform.Translate(move, Space.World);
    }
}
