using System;
using System.Drawing;
using Harmony.ColorSpace;

namespace Harmony
{
    internal static class HarmonyExtensions {
        internal static Color ToColor(this Hsl hsl) {
            Func<double, int> round = x => (int) Math.Round (x);
            var rgb = hsl.ToRgb ();
            return Color.FromArgb (round (rgb.R), round (rgb.G), round (rgb.B));
        }

        internal static Hsl ToHsl(this Color color) {
            return new Hsl (new Rgb (color.R, color.G, color.B));
        }
    }
}