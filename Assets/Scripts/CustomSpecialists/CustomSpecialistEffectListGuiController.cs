using UnityEngine;

namespace Rooms.Multiplayer.CustomSpecialists
{
    public class CustomSpecialistEffectListGuiController : MonoBehaviour
    {
        public GameObject scrollContent;
        public CustomSpecialistEffect specialistEffectTemplate;
        
        public void addSpecialistEffect()
        {
            var customSpecialistEffect = Instantiate(specialistEffectTemplate, scrollContent.transform);
            customSpecialistEffect.gameObject.SetActive(true);
        }
        
    }
}