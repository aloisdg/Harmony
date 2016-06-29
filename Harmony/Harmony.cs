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
    public class Harmony {
        // http://www.tigercolor.com/color-lab/color-theory/color-harmonies.htm
        // http://www.sensationalcolor.com/understanding-color/theory/color-relationships-creating-color-harmony-1849
        // http://www.chainstyle.com/tutorials/colscheme.html
        // http://www.easyrgb.com/index.php?X=WEEL
        private readonly Dictionary<Schemes, int[]> _schemes = new Dictionary<Schemes, int[]> {
            { Schemes.Complementary, new[] { 0, 6 }},
            { Schemes.SplitComplementary, new[] { 0, 5, 7 }},
            { Schemes.DoubleComplementaryRight, new[] { 0, 1, 6, 7 }},
            { Schemes.DoubleComplementaryLeft, new[] { 0, 5, 6, 11 }},
            { Schemes.Triadic, new[] { 0, 4, 8 }},
            { Schemes.TetradicRight, new[] { 0, 2, 6, 8 }},
            { Schemes.Tetradic, new[] { 0, 3, 6, 9 }}, // also called square
            { Schemes.TetradicLeft, new[] { 0, 4, 6, 10 }},
            { Schemes.AnalogousRight, new[] { 0, 1, 2 }},
            { Schemes.Analogous, new[] { 0, 1, 11 }},
            { Schemes.AnalogousLeft, new[] { 0, 10, 11 }},
            { Schemes.DiadRight, new[] { 0, 2 }},
            { Schemes.DiadLeft, new[] { 0, 10 }}
        };

        private readonly Dictionary<Schemes, string> _schemesName = new Dictionary<Schemes, string> {
            { Schemes.Complementary, "Complementary" },
            { Schemes.SplitComplementary, "Split Complementary" },
            { Schemes.DoubleComplementaryRight, "Double Complementary Right" },
            { Schemes.DoubleComplementaryLeft, "Double Complementary Left" },
            { Schemes.Triadic, "Triadic" },
            { Schemes.TetradicRight, "Tetradic Right" },
            { Schemes.Tetradic, "Tetradic" },
            { Schemes.TetradicLeft, "Tetradic Left" },
            { Schemes.AnalogousRight, "Analogous Right" },
            { Schemes.Analogous, "Analogous" },
            { Schemes.AnalogousLeft, "Analogous Left" },
            { Schemes.DiadRight, "Diad Right" },
            { Schemes.DiadLeft, "Diad Left" }
        };

        /// <summary>
        /// Color schemes come out of the color wheel and the different color schemes are
        /// different combinations of colors based on their relationship to each other.
        /// </summary>
        public enum Schemes {
            Complementary,
            SplitComplementary,
            DoubleComplementaryRight,
            DoubleComplementaryLeft,
            Triadic,
            TetradicRight,
            Tetradic,
            TetradicLeft,
            AnalogousRight,
            Analogous,
            AnalogousLeft,
            DiadRight,
            DiadLeft
        }

        private static IEnumerable<Color> Harmonize(Color color, IEnumerable<int> degrees) {
            var hsl = color.ToHsl ();
            return degrees.Select (degree => new Hsl {
                H = (360 + (hsl.H + 30 * degree)) % 360,
                S = hsl.S,
                L = hsl.L
            }.ToColor ());
        }

        // http://stackoverflow.com/questions/801406/c-create-a-lighter-darker-color-based-on-a-system-color
        //private static IEnumerable<Color> ScaleTo(Color color, double size, double scale) {
        //    Func<double, bool> isFinite = x => !double.IsInfinity (x) && !double.IsNaN (x);
        //    if (isFinite (size)) {
        //        size = 10;
        //    }
        //    var r = color.R;
        //    var g = color.G;
        //    var b = color.B;
        //    var scaleR = (scale - r) / size;
        //    var scaleG = (scale - g) / size;
        //    var scaleB = (scale - b) / size;
        //    for (var i = 0; i < size; i++) {
        //        yield return Color.FromArgb (r, g, b);
        //        r += scaleR;
        //        g += scaleG;
        //        b += scaleB;
        //    }
        //}

        //public void Add(string name, int[] degrees) {
        //    if (_schemes.ContainsKey (name)) //maybe useless
        //        throw new ArgumentException ("An item with the same key has already been added.");
        //    _schemes.Add (name, degrees);
        //}

        public IEnumerable<KeyValuePair<Schemes, IEnumerable<Color>>> HarmonizeAll(Color color) {
            return _schemes.Select (scheme =>
             new KeyValuePair<Schemes, IEnumerable<Color>> (scheme.Key, Harmonize (color, scheme.Value)));
        }

        public IEnumerable<Color> Harmonize(Color color, Schemes scheme) {
            if (!_schemes.ContainsKey (scheme))
                throw new KeyNotFoundException (nameof (scheme)); // useless?
            return Harmonize (color, _schemes[scheme]);
        }

        public string GetName(Schemes scheme) {
            return _schemesName[scheme];
        }

        //mix with black(#000000)
        //public IEnumerable<Color> GetShades(Color color, int size) {
        //    return ScaleTo (color, size, 0);
        //}

        //mix with white(#ffffff)
        //public IEnumerable<Color> GetTints(Color color, int size) {
        //    return ScaleTo (color, size, 1);
        //}

        //mix with middle gray(#777777)
        //public IEnumerable<Color> GetTones(Color color, int size) {
        //    return ScaleTo (color, size, 0.5);
        //}

        public IEnumerable<Color> GetMonochromatic(Color color) {
            Func<double, double, double, double> setBetween
                = (x, min, max) => x > min ? x < max ? x : max : min;
            Func<int, double> getLightness
                = x => ((double) x - 3) * 5;

            var hsl = color.ToHsl ();
            return Enumerable.Range (0, 7).Select (shade => new Hsl {
                H = hsl.H,
                S = hsl.S,
                L = setBetween (hsl.L + getLightness (shade), 0, 100)
            }.ToColor ());
        }

        public Temperature GetTemperature(Color color) {
            const int zero = 30;
            Func<double, double> moveToZero = x => (x + zero) % 360;
            var hsl = color.ToHsl ();
            return moveToZero (hsl.H) < 180 ? Temperature.Warm : Temperature.Cool;
        }

        public short GetTemperatureAsNumber(Color color) {
            const int limit = 100;
            const int maxWarm = 90;
            const int zero = 30;
            Func<double, double> moveToZero = x => (x + zero) % 360;
            var hue = moveToZero (Math.Round (color.ToHsl ().H));

            var shittyfix = false;
            if (hue > 180) {
                shittyfix = true;
                hue -= 180;
            }

            var temperature = limit - Math.Abs (maxWarm - hue) / maxWarm * limit;

            if (shittyfix)
                temperature = -temperature;
            return (short) temperature;
        }
    }
}