using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSpecialistGuiController : MonoBehaviour
{
    public Canvas SpecialistListCanvas;
    public Canvas PackageListCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void showSpecialistView()
    {
        SpecialistListCanvas.gameObject.SetActive(true);
        PackageListCanvas.gameObject.SetActive(false);
    }

    public void showPackageView()
    {
        SpecialistListCanvas.gameObject.SetActive(false);
        PackageListCanvas.gameObject.SetActive(true);
    }
}
