using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubterfugeCore;
using System;
using UnityEngine.UI;

public class TestDllScript : MonoBehaviour
{
    public Text textElement;
    
    // Start is called before the first frame update
    void Start()
    {   
        GameServer server = new GameServer();
        Debug.Log("There are " + GameServer.timeMachine.getState().getOutposts().Count.ToString() + " outposts.");
    }

    // Update is called once per frame
    void Update()
    {
        GameServer.timeMachine.advance(1);
        Debug.Log("Current tick: " + GameServer.timeMachine.getCurrentTick().getTick().ToString());
        textElement.text = "Current Tick: " + GameServer.timeMachine.getCurrentTick().getTick().ToString();
    }
}
