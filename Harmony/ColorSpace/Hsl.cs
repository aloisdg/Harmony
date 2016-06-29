using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Harmony.ColorSpace {
    internal class Hsl {
        public double H { get; }
        public double S { get; }
        public double L { get; }

        public Hsl(double h, double s, double l) {
            H = h;
            S = s;
            L = l;
        }

        public Hsl(Rgb rgb) {
            // TODO Losing precision
            var color = Color.FromArgb ((int) rgb.R, (int) rgb.G, (int) rgb.B);
            H = color.GetHue ();
            S = color.GetSaturation () * 100.0;
            L = color.GetBrightness () * 100.0;
        }

        public Rgb ToRgb() {
            var rangedH = H / 360.0;
            var r = 0.0;
            var g = 0.0;
            var b = 0.0;
            var s = S / 100.0;
            var l = L / 100.0;

            if (BasicallyEqualTo (l, 0))
                return new Rgb (255.0 * r, 255.0 * g, 255.0 * b);
            if (BasicallyEqualTo (s, 0))
                r = g = b = l;
            else {
                var temp2 = l < 0.5 ? l * (1.0 + s) : l + s - l * s;
                var temp1 = 2.0 * l - temp2;

                r = GetColorComponent (temp1, temp2, rangedH + 1.0 / 3.0);
                g = GetColorComponent (temp1, temp2, rangedH);
                b = GetColorComponent (temp1, temp2, rangedH - 1.0 / 3.0);
            }
            return new Rgb (255.0 * r, 255.0 * g, 255.0 * b);
        }

        private static double GetColorComponent(double temp1, double temp2, double temp3) {
            temp3 = MoveIntoRange (temp3);
            if (temp3 < 1.0 / 6.0)
                return temp1 + (temp2 - temp1) * 6.0 * temp3;

            if (temp3 < 0.5)
                return temp2;

            if (temp3 < 2.0 / 3.0)
                return temp1 + (temp2 - temp1) * (2.0 / 3.0 - temp3) * 6.0;

            return temp1;
        }

        private static double MoveIntoRange(double temp3) {
            if (temp3 < 0.0)
                return temp3 + 1.0;
            if (temp3 > 1.0)
                return temp3 - 1.0;
            return temp3;
        }

        private const double DefaultPrecision = .0001;

        private static bool BasicallyEqualTo(double a, double b, double precision = DefaultPrecision) {
            return Math.Abs (a - b) <= precision;
        }
    }
}