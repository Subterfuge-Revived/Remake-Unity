using SubterfugeCore.Core.Components;
using UnityEngine;

public class SpecialistCarrier : MonoBehaviour
 {
     private SpecialistManager specialistCarrier;
    
     // Start is called before the first frame update
     void Start()
     {
         OutpostManager outpostManager = gameObject.GetComponent<OutpostManager>();
         if (outpostManager != null)
         {
             specialistCarrier = outpostManager.outpost.GetComponent<SpecialistManager>();
         }
         else
         {
             specialistCarrier = gameObject.GetComponentInParent<SubManager>().sub.GetComponent<SpecialistManager>();
         }
     }

     // Update is called once per frame
     void Update()
     {
         // Show all specialists above the object.
         // foreach (Specialist s in specialistCarrier.GetSpecialistManager().GetSpecialists())
         // {
         //     SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
         //     renderer.sprite = Resources.Load<Sprite>("Specialists/Queen");
         //     Vector3 scale = gameObject.GetComponent<Transform>().localScale;
         //     scale.Set(15, 15, 1);
         // }
     }
 }