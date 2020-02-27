using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int frameCounter = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCounter % 15 == 0)
        {
            // Advance the game but at a slow-ish rate
            Game.timeMachine.advance(1);
            frameCounter = 1;
        }

        frameCounter++;
    }
}
