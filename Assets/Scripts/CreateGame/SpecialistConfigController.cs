using System.Collections.Generic;
using SubterfugeCore.Models.GameEvents;
using UnityEngine;

public class SpecialistConfigController : MonoBehaviour
{
    private List<SpecialistIconDisplay> SelectedSpecialists = new List<SpecialistIconDisplay>();

    public GameObject specialistImportPopup;
    public GameObject specialistImportControl;

    public void loadFromConfig(List<SpecialistConfiguration> specialistConfig)
    {
        disableInput();
        // SelectedSpecialists.AddRange(specialistConfig);
    }

    public void disableInput()
    {
        specialistImportControl.SetActive(false);
        specialistImportPopup.SetActive(false);
    }

    public void showSpecialistImport()
    {
        specialistImportPopup.SetActive(true);
    }

    public void hideSpecailistImport()
    {
        specialistImportPopup.SetActive(false);
    }

    public void toggleSpecialist(SpecialistIconDisplay specialist)
    {
        if (specialist.isActive)
        {
            SelectedSpecialists.Add(specialist);
            Debug.unityLogger.Log("Selected!");
        }
        else
        {
            SelectedSpecialists.Remove(specialist);
            Debug.unityLogger.Log("Removed!");
        }
    }

    public List<SpecialistConfiguration> getConfiguredValues()
    {
        var configList = new List<SpecialistConfiguration>();
        foreach(SpecialistIconDisplay specialist in SelectedSpecialists)
        {
            configList.Add(new SpecialistConfiguration()
            {
                Id = "SomeId",
                Priority = 1,
                SpecialistName = "SpecialistName",
                Creator = new SimpleUser()
                {
                    Id = "SomeUserId",
                    Username = "SomeUserName",
                },
            });
        }

        return configList;
    }
}
