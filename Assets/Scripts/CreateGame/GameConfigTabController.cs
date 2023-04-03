using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigTabController : MonoBehaviour
{
    public GameObject generalConfigDisplay;
    public GameObject mapConfigDisplay;
    public GameObject specialistConfigDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        showGeneralConfig();
    }

    public void showGeneralConfig()
    {
        disableAll();
        generalConfigDisplay.SetActive(true);
    }

    public void showMapConfig()
    {
        disableAll();
        mapConfigDisplay.SetActive(true);
    }

    public void showSpecialistConfig()
    {
        disableAll();
        specialistConfigDisplay.SetActive(true);
    }

    private void disableAll()
    {
        generalConfigDisplay.SetActive(false);
        mapConfigDisplay.SetActive(false);
        specialistConfigDisplay.SetActive(false);
    }
}
