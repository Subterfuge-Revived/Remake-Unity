using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Entities;
using SubterfugeCore.Core.Entities.Locations;
using SubterfugeCore.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class SouceLaunchInformation : MonoBehaviour
{
    public Outpost source;

    // Update is called once per frame
    void Update()
    {
        Text text = gameObject.GetComponentInChildren<Text>();
        text.text = "====Source Outpost====\n" +
                    "Outpost Id: " + source.getId() + "\n" +
                    "Shields: " + source.getShields() + "\n" +
                    "Drillers: " + source.getDrillerCount() + "\n" +
                    "Specialists: " + source.getSpecialistManager().getSpecialistCount();
    }
}
