using System;
using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AdvanceTimemachine(int ticks)
    {
        Game.timeMachine.advance(ticks);
    }
}
