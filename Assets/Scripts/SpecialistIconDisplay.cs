
using System;
using SubterfugeCore.Core.Entities.Specialists;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistIconDisplay : MonoBehaviour
{

    public Specialist Specialist;
    public Image image;
    public Image SpecialistIconTint;
    public bool isActive = true;

    public void Start()
    {
        setSpecialistSprite();
    }
    
    private void setSpecialistSprite()
    {
        // TODO: Add Specialist.getName() in core library.
        image.sprite = Resources.Load<Sprite>($"Specialists/Queen");
    }

    public void OnSelect()
    {
        isActive = !isActive;
        SpecialistIconTint.gameObject.SetActive(!isActive);
        if (!isActive)
        {
            var tempColor = SpecialistIconTint.color;
            tempColor.a = 0.75f;
            SpecialistIconTint.color = tempColor;
        }
    }

}