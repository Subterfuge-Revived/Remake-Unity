using System.Collections;
using System.Collections.Generic;
using SubterfugeCore.Core.Entities.Specialists;
using SubterfugeRemakeService;
using UnityEngine;
using SpecialistConfiguration = SubterfugeRemakeService.SpecialistConfiguration;

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

    public List<SubterfugeRemakeService.SpecialistConfiguration> getConfiguredValues()
    {
        var configList = new List<SubterfugeRemakeService.SpecialistConfiguration>();
        foreach(SpecialistIconDisplay specialist in SelectedSpecialists)
        {
            configList.Add(new SubterfugeRemakeService.SpecialistConfiguration()
            {
                Id = "SomeId",
                Priority = 1,
                SpecialistName = "SpecialistName",
                Creator = new User()
                {
                    Id = "SomeUserId",
                    Username = "SomeUserName",
                },
            });
        }

        return configList;
    }
}
