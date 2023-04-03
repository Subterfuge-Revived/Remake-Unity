using UnityEngine;

namespace Rooms.Multiplayer
{
    public class GlobalColorScheme
    {
        private static ColorScheme _colorScheme = ColorScheme.Of(ColorPallet.Cadet);

        public static ColorScheme getColorScheme()
        {
            if (_colorScheme == null)
            {
                _colorScheme = ColorScheme.Of(ColorPallet.Cadet);
            }

            return _colorScheme;
        }

        public static void SetColorScheme(ColorScheme newScheme)
        {
            if (newScheme != null)
            {
                _colorScheme = newScheme;
            }
        }
    }
}