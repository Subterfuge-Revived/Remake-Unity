using Subterfuge.Remake.Core.Entities.Components;
using Subterfuge.Remake.Core.Entities.Positions;
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
                    "Outpost Id: " + source.GetComponent<IdentityManager>().GetId() + "\n" +
                    "Shields: " + source.GetComponent<ShieldManager>().GetShields() + "\n" +
                    "Drillers: " + source.GetComponent<DrillerCarrier>().GetDrillerCount() + "\n" +
                    "Specialists: " + source.GetComponent<SpecialistManager>().GetUncapturedSpecialistCount();
    }
}
