using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEmblemCreator : MonoBehaviour
{
    public PlayerEmblemType emblemType;
    public Color emblemColor;
    public Color backgroundColor;

    public Image background;
    public Image emblem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        background.color = backgroundColor;
        emblem.color = GetBoundedColor(emblemColor);
        emblem.sprite = GetEmblemSprite();
    }
    
    private Color GetBoundedColor(Color inputColor)
    {
        // Ensure the foreground is not 'too' white. Otherwise the text gets blended and becomes unreadable.
        // Cast to HSV to determine the saturation.
        float hue;
        float satuation;
        float value;
        Color.RGBToHSV(inputColor, out hue, out satuation, out value);
        
        // Ensure that saturation is above 40
        satuation = Math.Max(0.40f, satuation);
        value = Math.Min(0.80f, value);
        
        // Convert back to RGB
        return Color.HSVToRGB(hue, satuation, value);
    }

    public Sprite GetEmblemSprite()
    {
        switch (emblemType)
        {
            case PlayerEmblemType.Anchor:
                return Resources.Load<Sprite>("Profile/Game-Emblems/AnchorEmblem");
            case PlayerEmblemType.Arrow:
                return Resources.Load<Sprite>("Profile/Game-Emblems/ArrowEmblem");
            case PlayerEmblemType.Asterick:
                return Resources.Load<Sprite>("Profile/Game-Emblems/AsterickEmblem");
            case PlayerEmblemType.CrossFray:
                return Resources.Load<Sprite>("Profile/Game-Emblems/CrossFrayEmblem");
            case PlayerEmblemType.Fairy:
                return Resources.Load<Sprite>("Profile/Game-Emblems/FairyEmblem");
            case PlayerEmblemType.Heart:
                return Resources.Load<Sprite>("Profile/Game-Emblems/HeartEmblem");
            case PlayerEmblemType.HourglassArrow:
                return Resources.Load<Sprite>("Profile/Game-Emblems/HourglassArrowEmblem");
            case PlayerEmblemType.Kite:
                return Resources.Load<Sprite>("Profile/Game-Emblems/KiteEmblem");
            case PlayerEmblemType.Leaf:
                return Resources.Load<Sprite>("Profile/Game-Emblems/LeafEmblem");
            case PlayerEmblemType.Oval:
                return Resources.Load<Sprite>("Profile/Game-Emblems/OvalEmblem");
            case PlayerEmblemType.Pinwheel:
                return Resources.Load<Sprite>("Profile/Game-Emblems/Pinwheel");
            case PlayerEmblemType.Pitchfork:
                return Resources.Load<Sprite>("Profile/Game-Emblems/PitchforkEmblem");
            case PlayerEmblemType.Pyramid:
                return Resources.Load<Sprite>("Profile/Game-Emblems/PyramidEmblem");
            case PlayerEmblemType.Scraper:
                return Resources.Load<Sprite>("Profile/Game-Emblems/ScraperEmblem");
            case PlayerEmblemType.Star:
                return Resources.Load<Sprite>("Profile/Game-Emblems/StarEmblem");
            default:
                return Resources.Load<Sprite>("Profile/Game-Emblems/StarEmblem");
        }
    }
    
}

public enum PlayerEmblemType
{
    None = 0,
    Anchor = 1,
    Arrow = 2,
    Asterick = 3,
    CrossFray = 4,
    Fairy = 5,
    Heart = 6,
    HourglassArrow = 7,
    Kite = 8,
    Leaf = 9,
    Oval = 10,
    Pinwheel = 11,
    Pitchfork = 12,
    Pyramid = 13,
    Scraper = 14,
    Star = 15,
}
