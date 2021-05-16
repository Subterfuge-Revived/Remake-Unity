
using System;
using SubterfugeCore.Core.Entities.Specialists;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistIconDisplay : MonoBehaviour
{

    public Specialist Specialist;
    public Image image;

    public void Start()
    {
        setSpecialistSprite();
    }
    
    private void setSpecialistSprite()
    {
        // TODO: Add Specialist.getName() in core library.
        image.sprite = Resources.Load<Sprite>($"Specialists/Queen");
    }

}