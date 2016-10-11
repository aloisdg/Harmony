using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Harmony.ColorSpace;

// inspiration
// https://github.com/skratchdot/color-harmony

// color picker
// http://www.colorhexa.com/c715f0 // best by far
// https://duckduckgo.com/?q=color+picker+%23c715f0&ia=colorpicker
// http://www.sessions.edu/color-calculator

// Others
// http://programmers.stackexchange.com/questions/44929/color-schemes-generation-theory-and-algorithms

namespace Harmony {
    public struct Harmony {
        public readonly IReadOnlyList<Color> Complementary;
        public readonly IReadOnlyList<Color> SplitComplementary;
        public readonly IReadOnlyList<Color> DoubleComplementaryRight;
        public readonly IReadOnlyList<Color> DoubleComplementaryLeft;
        public readonly IReadOnlyList<Color> Triadic;
        public readonly IReadOnlyList<Color> TetradicRight;
        public readonly IReadOnlyList<Color> Tetradic;
        public readonly IReadOnlyList<Color> TetradicLeft;
        public readonly IReadOnlyList<Color> AnalogousRight;
        public readonly IReadOnlyList<Color> Analogous;
        public readonly IReadOnlyList<Color> AnalogousLeft;
        public readonly IReadOnlyList<Color> DiadRight;
        public readonly IReadOnlyList<Color> DiadLeft;

        public readonly IReadOnlyList<Color> Monochromatic;
        public readonly Temperature Temperature;
        public readonly short TemperatureDegree;

        private static IEnumerable<Color> BuildHarmonies(IHsl hsl) => Enumerable.Range (0, 12).Select (degree => new Hsl {
            H = (360 + (hsl.H + 30 * degree)) % 360,
            S = hsl.S,
            L = hsl.L
        }.ToColor ());

        private static IEnumerable<Color> BuildMonochomatic(IHsl hsl) => Enumerable.Range (0, 7).Select (shade => new Hsl {
            H = hsl.H,
            S = hsl.S,
            L = Limit (hsl.L + Enlight (shade), 0, 100)
        }.ToColor ());

        private static double Limit(double x, double min, double max) => x > min ? x < max ? x : max : min;

        private static double Enlight(int x) => ((double) x - 3) * 5;

        private const int TemperatureZero = 30;

        private static double MoveToZero(double x) => (x + TemperatureZero) % 360;

        public Harmony(Color color) {
            var hsl = color.ToHsl ();
            var harmonies = BuildHarmonies (hsl).ToArray ();

            Complementary = new[] { harmonies[0], harmonies[6] };
            SplitComplementary = new[] { harmonies[0], harmonies[5], harmonies[7] };
            DoubleComplementaryRight = new[] { harmonies[0], harmonies[1], harmonies[6], harmonies[7] };
            DoubleComplementaryLeft = new[] { harmonies[0], harmonies[5], harmonies[6], harmonies[11] };
            Triadic = new[] { harmonies[0], harmonies[4], harmonies[8] };
            TetradicRight = new[] { harmonies[0], harmonies[2], harmonies[6], harmonies[8] };
            Tetradic = new[] { harmonies[0], harmonies[3], harmonies[6], harmonies[9] };
            TetradicLeft = new[] { harmonies[0], harmonies[4], harmonies[6], harmonies[10] };
            AnalogousRight = new[] { harmonies[0], harmonies[1], harmonies[2] };
            Analogous = new[] { harmonies[0], harmonies[1], harmonies[11] };
            AnalogousLeft = new[] { harmonies[0], harmonies[10], harmonies[11] };
            DiadRight = new[] { harmonies[0], harmonies[2] };
            DiadLeft = new[] { harmonies[0], harmonies[10] };

            Monochromatic = BuildMonochomatic (hsl).ToArray ();

            Temperature = MoveToZero (hsl.H) < 180 ? Temperature.Warm : Temperature.Cool;

            const int limit = 100;
            const int maxWarm = 90;
            var hue = MoveToZero (Math.Round (color.ToHsl ().H));

            var shittyfix = false;
            if (hue > 180) {
                shittyfix = true;
                hue -= 180;
            }
            var temperature = limit - Math.Abs (maxWarm - hue) / maxWarm * limit;

            if (shittyfix)
                temperature = -temperature;
            TemperatureDegree = (short) temperature;
        }
    }
}