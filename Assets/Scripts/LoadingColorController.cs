using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms.Multiplayer
{
    public class LoadingColorController : MonoBehaviour
    {
        public ColorTheme theme;
        
        public Image OuterImage;
        public Image MiddleImage;
        public Image InnerImage;
        public Image CenterDot;
        
        
        void Start() {
            switch (theme)
            {
                case ColorTheme.White:
                    setImageColors(Color.white, Color.white, Color.white, Color.white);
                    break;
                case ColorTheme.Rainbow:
                    setImageColors("E50000".ToColor(), "FF8D00".ToColor(), "028121".ToColor(),"770088".ToColor());
                    break;
                case ColorTheme.Transgender:
                    setImageColors("5BCFFB".ToColor(), "F5ABB9".ToColor(), Color.white, Color.white);
                    break;
                case ColorTheme.Bisexual:
                    setImageColors("D60270".ToColor(), "9B4F96".ToColor(), "0038A8".ToColor(), Color.white);
                    break;
                case ColorTheme.Pansexual:
                    setImageColors("FF1C8D".ToColor(), "FFD700".ToColor(), "1AB3FF".ToColor(), Color.white);
                    break;
                case ColorTheme.Nonbinary:
                    setImageColors("FCF431".ToColor(), "FCFCFC".ToColor(), "9D59D2".ToColor(), "282828".ToColor());
                    break;
                case ColorTheme.Lesbian:
                    setImageColors("D62800".ToColor(), "FF9B56".ToColor(), "D462A6".ToColor(), "A40062".ToColor());
                    break;
                case ColorTheme.Agender:
                    setImageColors(Color.black, "BABABA".ToColor(), Color.white, "BAF484".ToColor());
                    break;
                case ColorTheme.Asexual:
                    setImageColors(Color.black, "A4A4A4".ToColor(), Color.white, "810081".ToColor());
                    break;
                case ColorTheme.Genderqueer:
                    setImageColors("B57FDD".ToColor(), Color.white, "49821E".ToColor(), Color.white);
                    break;
                case ColorTheme.Genderfluid:
                    setImageColors("FE76A2".ToColor(), Color.white, "BF12D7".ToColor(), "303CBE".ToColor());
                    break;
                case ColorTheme.Intersex:
                    setImageColors("FFD800".ToColor(), "FFD800".ToColor(), "7902AA".ToColor(), "FFD800".ToColor());
                    break;
                case ColorTheme.Aromantic:
                    setImageColors("3BA740".ToColor(), "A8D47A".ToColor(), "ABABAB".ToColor(), Color.black);
                    break;
                case ColorTheme.Polyamorous:
                    setImageColors(Color.blue, Color.red, Color.black, Color.yellow);
                    break;
            }
        }

        void Update()
        {
         switch (theme)
            {
                case ColorTheme.White:
                    setImageColors(Color.white, Color.white, Color.white, Color.white);
                    break;
                case ColorTheme.Rainbow:
                    setImageColors("E50000".ToColor(), "FF8D00".ToColor(), "028121".ToColor(),"770088".ToColor());
                    break;
                case ColorTheme.Transgender:
                    setImageColors("5BCFFB".ToColor(), "F5ABB9".ToColor(), Color.white, Color.white);
                    break;
                case ColorTheme.Bisexual:
                    setImageColors("D60270".ToColor(), "9B4F96".ToColor(), "0038A8".ToColor(), Color.white);
                    break;
                case ColorTheme.Pansexual:
                    setImageColors("FF1C8D".ToColor(), "FFD700".ToColor(), "1AB3FF".ToColor(), Color.white);
                    break;
                case ColorTheme.Nonbinary:
                    setImageColors("FCF431".ToColor(), "FCFCFC".ToColor(), "9D59D2".ToColor(), "282828".ToColor());
                    break;
                case ColorTheme.Lesbian:
                    setImageColors("D62800".ToColor(), "FF9B56".ToColor(), "D462A6".ToColor(), "A40062".ToColor());
                    break;
                case ColorTheme.Agender:
                    setImageColors(Color.black, "BABABA".ToColor(), Color.white, "BAF484".ToColor());
                    break;
                case ColorTheme.Asexual:
                    setImageColors(Color.black, "A4A4A4".ToColor(), Color.white, "810081".ToColor());
                    break;
                case ColorTheme.Genderqueer:
                    setImageColors("B57FDD".ToColor(), Color.white, "49821E".ToColor(), Color.white);
                    break;
                case ColorTheme.Genderfluid:
                    setImageColors("FE76A2".ToColor(), Color.white, "BF12D7".ToColor(), "303CBE".ToColor());
                    break;
                case ColorTheme.Intersex:
                    setImageColors("FFD800".ToColor(), "FFD800".ToColor(), "7902AA".ToColor(), "FFD800".ToColor());
                    break;
                case ColorTheme.Aromantic:
                    setImageColors("3BA740".ToColor(), "A8D47A".ToColor(), "ABABAB".ToColor(), Color.black);
                    break;
                case ColorTheme.Polyamorous:
                    setImageColors(Color.blue, Color.red, Color.black, Color.yellow);
                    break;
            }
        }

        private void setImageColors(Color outer, Color middle, Color inner, Color centerDot)
        {
            OuterImage.color = outer;
            MiddleImage.color = middle;
            InnerImage.color = inner;
            CenterDot.color = centerDot;
        }
        
    }

    public enum ColorTheme
    {
        White,
        Rainbow,
        Transgender,
        Bisexual,
        Pansexual,
        Nonbinary,
        Lesbian,
        Agender,
        Asexual,
        Genderqueer,
        Genderfluid,
        Intersex,
        Aromantic,
        Polyamorous
    }
    
    public static class ColorExtensions
    {
        public static Color ToColor(this string color)
        {
            if (color.StartsWith("#", StringComparison.InvariantCulture))
            {
                color = color.Substring(1); // strip #
            }

            if (color.Length == 6)
            {
                color += "FF"; // add alpha if missing
            }
            var hex = Convert.ToUInt32(color, 16);
            var r = ((hex & 0xff000000) >> 0x18) / 255f;
            var g = ((hex & 0xff0000) >> 0x10) / 255f;
            var b = ((hex & 0xff00) >> 8) / 255f;
            var a = ((hex & 0xff)) / 255f;

            return new Color(r, g, b, a);
        }
    }
}