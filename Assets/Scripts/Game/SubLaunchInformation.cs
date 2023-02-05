using SubterfugeCore.Core.Components;
using SubterfugeCore.Core.Entities;
using SubterfugeCore.Core.Entities.Positions;
using UnityEngine;
using UnityEngine.UI;

public class SubLaunchInformation : MonoBehaviour
{

    public Outpost sourceOutpost;
    public Entity destination;
    public Slider slider;
    public Text SubInformation;
    public 
    
    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = sourceOutpost.GetComponent<DrillerCarrier>().GetDrillerCount();
    }

    // Update is called once per frame
    void Update()
    {
        SubInformation.text = "Drillers: " + slider.value + "\n" +
                              "Specialists: \n" +
                              "Destination: " + destination;
    }
}
