using System;
using Rooms.Multiplayer.PlayerProfile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileCreator : MonoBehaviour
{

    public PlayerProfileBackground PlayerProfileBackground = PlayerProfileBackground.Solid;
    public Color backgroundColor = Color.red;
    public Color backgroundSecondary = Color.white;
    
    public String playerTitle = "Quinntonium";
    public Color bannerTextColor = Color.yellow;

    public Image foreground;
    public Image background;
    public TextMeshProUGUI bannerText;
    
    private SpriteRenderer renderer;
    
    // TODO: Border

    // Start is called before the first frame update
    void Start()
    {
        foreground.sprite = getBackgroundSprite();
        foreground.color = backgroundColor;
        background.color = backgroundSecondary;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the top alpha is 49 so that it blends to the background (if it is a gradient)
        backgroundColor.a = 49;
        // Ensure the background is solid.
        backgroundSecondary.a = 255;
        
        backgroundColor = GetBoundedColor(backgroundColor);
        foreground.sprite = getBackgroundSprite();
        foreground.color = backgroundColor;
        background.color = backgroundSecondary;
        
        // Set the player title
        bannerText.text = playerTitle;
        bannerText.outlineWidth = 1f;
        bannerText.outlineColor = GetBoundedColor(bannerTextColor);
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

    private Sprite getBackgroundSprite()
    {
        switch (PlayerProfileBackground)
        {
            case PlayerProfileBackground.Solid:
                return Resources.Load<Sprite>("Profile/Backgrounds/Solid");
            case PlayerProfileBackground.GradientDiagonal:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientDiagonal");
            case PlayerProfileBackground.GradientHorizontal:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientHorizontal");
            case PlayerProfileBackground.GradientRadial:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientRadial");
            case PlayerProfileBackground.GradientSpiral:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientSpiral");
            case PlayerProfileBackground.GradientStar:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientStar");
            case PlayerProfileBackground.GradientMiddleOut:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientMiddleOut");
            case PlayerProfileBackground.GradientConicalCorner:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientConicalCorner");
            case PlayerProfileBackground.GradientConicalEdge:
                return Resources.Load<Sprite>("Profile/Backgrounds/GradientConicalEdge");
            default:
                return Resources.Load<Sprite>("Profile/Backgrounds/Solid");
        }
    }
}
