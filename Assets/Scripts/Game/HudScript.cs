using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core;
using TMPro;
using UnityEngine;

public class HudScript : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "GameTick: " + Game.timeMachine.getCurrentTick().getTick().ToString();
    }
}
