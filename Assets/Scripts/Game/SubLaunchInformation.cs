using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Entities.Locations;
using SubterfugeCore.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class SubLaunchInformation : MonoBehaviour
{

    public Outpost sourceOutpost;
    public ITargetable destination;
    public Slider slider;
    public Text SubInformation;
    public 
    
    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = sourceOutpost.getDrillerCount();
    }

    // Update is called once per frame
    void Update()
    {
        SubInformation.text = "Drillers: " + slider.value + "\n" +
                              "Specialists: \n" +
                              "Destination: " + destination.getId();
    }
}
