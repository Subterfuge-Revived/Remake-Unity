using System;
using System.Linq;
using Subterfuge.Remake.Core.Entities.Components;
using TMPro;
using UnityEngine;

public class SpecialistCarrier : MonoBehaviour
 {
     private SpecialistManager specialistCarrier;
     public TextMeshPro specialistText;
    
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
         var specialists = String.Join(",", specialistCarrier.GetSpecialists().Select(it => it.GetSpecialistId().ToString()));
         specialistText.text = specialists;
     }
 }