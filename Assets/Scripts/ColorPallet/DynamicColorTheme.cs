using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer
{
    public class DynamicColorTheme : MonoBehaviour
    {
        
        // Color theme for the app.
        // This component will dynamically color images, text, icons, based on the theme colors provided here.
        public ColorType ColorMode = ColorType.TextHeading;

        public void Update()
        {
            switch (ColorMode)
            {
                case ColorType.Accent:
                    // Color Icons and text.
                    SetColorIfNotNull(GetComponent<Image>());
                    SetColorIfNotNull(GetComponent<TextMeshPro>());
                    SetColorIfNotNull(GetComponent<TextMeshProUGUI>());
                    break;
                case ColorType.Background:
                    // Color images.
                    SetColorIfNotNull(GetComponent<Image>());
                    break;
                case ColorType.TextDescription:
                    SetColorIfNotNull(GetComponent<TextMeshPro>());
                    SetColorIfNotNull(GetComponent<TextMeshProUGUI>());
                    break;
                case ColorType.TextHeading:
                    SetColorIfNotNull(GetComponent<TextMeshPro>());
                    SetColorIfNotNull(GetComponent<TextMeshProUGUI>());
                    break;
            }
        }

        private void SetColorIfNotNull(Graphic any)
        {
            if (any != null)
            {
                Debug.Log("Setting color for" + ColorMode);
                any.color = GlobalColorScheme.getColorScheme().GetColorForType(ColorMode);
            }
        }
    }

    public class ColorScheme
    {
        private Color BgLight;
        private Color BgDark;
        private Color TextLight;
        private Color TextDark;
        private Color Accent;

        public bool DarkMode { get; set; } = false;

        public ColorScheme(
            Color bgLight,
            Color textLight,
            Color bgDark,
            Color textDark,
            Color accent
        )
        {
            BgLight = bgLight;
            BgDark = bgDark;
            TextLight = textLight;
            Accent = accent;
            TextDark = textDark;
        }

        public static ColorScheme Of(ColorPallet pallet)
        {
            switch (pallet)
            {
                case ColorPallet.Cadet:
                default:
                    return new ColorScheme(
                        Color.white,
                        new Color(0.9f, 0.9f, 0.9f),
                        Color.black,
                        new Color(0.08f, 0.13f, 0.24f),
                        new Color(0.99f, 0.64f, 0.07f)
                    );
                    
            }
        }

        public Color GetColorForType(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Accent:
                    return Accent;
                case ColorType.Background:
                    if (DarkMode)
                    {
                        return BgDark;
                    }

                    return BgLight;
                case ColorType.TextHeading:
                    if (DarkMode)
                    {
                        return BgLight;
                    }

                    return BgDark;
                case ColorType.TextDescription:
                    if (DarkMode)
                    {
                        return TextLight;
                    }

                    return TextDark;
                default:
                    return Accent;
            }
        }
        
    }

    public enum ColorType
    {
        Background,
        TextHeading,
        TextDescription,
        Accent,
    }

    public enum ColorPallet
    {
        Cadet,
    }
    
}