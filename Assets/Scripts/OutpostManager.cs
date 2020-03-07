﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubterfugeCore.Core;
 using SubterfugeCore.Core.Entities.Locations;
 using TMPro;
 using UnityEditor.UI;
 using UnityEngine.UI;
 using UnityEngine.XR;
 using Object = System.Object;

 public class OutpostManager : MonoBehaviour
{

    private Animator OutpostAnimator;
    public System.Guid ID;
    private float downtime;
    private bool expanded = false;
    private TextMeshPro textMesh;

    public Outpost outpost;
    // Start is called before the first frame update
    void Start()
    {
        OutpostAnimator = gameObject.GetComponent<Animator>();
        textMesh = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set color based on the owner
        textMesh.text = outpost.getDrillerCount().ToString();

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int playerId = 0;
        if (outpost.getOwner() != null)
        {
            playerId = outpost.getOwner().getId();
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
        
        if (Input.GetMouseButtonDown(0))
        {
            downtime = Time.time;
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            downtime = Time.time - downtime;
        }
        
        if (Input.GetMouseButton(0))
        {
            return;
        }

        if (downtime < 0.25 && downtime > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject && expanded == false)
                {
                    hit.collider.gameObject.GetComponent<OutpostManager>().Expand();
                    //gameManager.SelectedOutpost = hit.transform.GetComponent<OutpostManager>().ID;
                }
                else
                {
                    gameObject.GetComponent<OutpostManager>().Contract();
                }
            }
            else
            {
                gameObject.GetComponent<OutpostManager>().Contract();
            }
            
            downtime = 0;
        }
    }

    public void Expand()
    {
        expanded = true;
        OutpostAnimator.ResetTrigger("Contract");
        OutpostAnimator.SetTrigger("Expand");
    }
    
    public void Contract()
    {
        expanded = false;
        OutpostAnimator.ResetTrigger("Expand");
        OutpostAnimator.SetTrigger("Contract");
    }
}
