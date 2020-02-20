﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubterfugeCore.Core;
using UnityEditor.UI;
using UnityEngine.XR;

public class OutpostManager : MonoBehaviour
{

    private Animator OutpostAnimator;
    public System.Guid ID;
    private float downtime;
    private bool expanded = false;
    // Start is called before the first frame update
    void Start()
    {
        OutpostAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    Debug.Log("Expand: " + gameObject.name);
                    hit.collider.gameObject.GetComponent<OutpostManager>().Expand();
                    //gameManager.SelectedOutpost = hit.transform.GetComponent<OutpostManager>().ID;
                }
                else
                {
                    Debug.Log("Contract: " + gameObject.name);
                    gameObject.GetComponent<OutpostManager>().Contract();
                }
            }
            else
            {
                Debug.Log("Contract: " + gameObject.name);
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
