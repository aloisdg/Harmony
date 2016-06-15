using System;
using System.Drawing;

namespace Harmony
{
    internal static class HarmonyExtensions {
        internal static Color ToColor(this Hsl hsl) {
            Func<double, int> round = x => (int) Math.Round (x);
            var rgb = hsl.ToRgb ();
            return Color.FromArgb (round (rgb.R), round (rgb.G), round (rgb.B));
        }

        internal static Hsl ToHsl(this Color color) {
            var hsl = new Hsl ();
            hsl.Initialize (new Rgb { R = color.R, G = color.G, B = color.B });
            return hsl;
        }
    }
}