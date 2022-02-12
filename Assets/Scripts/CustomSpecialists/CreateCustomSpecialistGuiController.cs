using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateCustomSpecialistGuiController : MonoBehaviour
{
    public Canvas GeneralCustomizationCanvas;
    public Canvas SpecialistEffectCustomizationCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void toSpecialistMenu()
    {
        SceneManager.LoadScene("CustomSpecialistInformation");
    }

    public void showGeneralCustomization()
    {
        GeneralCustomizationCanvas.gameObject.SetActive(true);
        SpecialistEffectCustomizationCanvas.gameObject.SetActive(false);
    }

    public void showSpecialistEffectCustomizationCanvas()
    {
        SpecialistEffectCustomizationCanvas.gameObject.SetActive(true);
        GeneralCustomizationCanvas.gameObject.SetActive(false);
    }
}
