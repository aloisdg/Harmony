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

// Harmonies
// http://www.tigercolor.com/color-lab/color-theory/color-harmonies.htm
// http://www.sensationalcolor.com/understanding-color/theory/color-relationships-creating-color-harmony-1849
// http://www.chainstyle.com/tutorials/colscheme.html
// http://www.easyrgb.com/index.php?X=WEEL

namespace Harmony {
    public class Harmony {
        protected abstract class Scheme {
            public string Name { get; }
            public IEnumerable<int> Degrees { get; }

            protected Scheme(string name, IEnumerable<int> degrees) {
                Name = name;
                Degrees = degrees;
            }

            public override string ToString() {
                return Name;
            }
        }

        private class Complementary : Scheme {
            public Complementary() : base ("Complementary", new[] { 0, 6 }) { }
        }

        private class SplitComplementary : Scheme {
            public SplitComplementary() : base ("Split Complementary", new[] { 0, 5, 7 }) { }
        }

        private class DoubleComplementaryRight : Scheme {
            public DoubleComplementaryRight() : base ("Double Complementary Right", new[] { 0, 1, 6, 7 }) { }
        }

        private class DoubleComplementaryLeft : Scheme {
            public DoubleComplementaryLeft() : base ("Double Complementary Left", new[] { 0, 5, 6, 11 }) { }
        }

        private class Triadic : Scheme {
            public Triadic() : base ("Triadic", new[] { 0, 4, 8 }) { }
        }

        private class TetradicRight : Scheme {
            public TetradicRight() : base ("Tetradic Right", new[] { 0, 2, 6, 8 }) { }
        }

        // also called square
        private class Tetradic : Scheme {
            public Tetradic() : base ("Tetradic", new[] { 0, 3, 6, 9 }) { }
        }

        private class TetradicLeft : Scheme {
            public TetradicLeft() : base ("Tetradic Left", new[] { 0, 4, 6, 10 }) { }
        }

        private class AnalogousRight : Scheme {
            public AnalogousRight() : base ("Analogous Right", new[] { 0, 1, 2 }) { }
        }

        private class Analogous : Scheme {
            public Analogous() : base ("Analogous", new[] { 0, 1, 11 }) { }
        }

        private class AnalogousLeft : Scheme {
            public AnalogousLeft() : base ("Analogous Left", new[] { 0, 10, 11 }) { }
        }

        private class DiadRight : Scheme {
            public DiadRight() : base ("Diad Right", new[] { 0, 2 }) { }
        }

        private class DiadLeft : Scheme {
            public DiadLeft() : base ("Diad Left", new[] { 0, 10 }) { }
        }

        private readonly Dictionary<Schemes, Scheme> _schemes = new Dictionary<Schemes, Scheme> {
            { Schemes.Complementary, new Complementary()},
            { Schemes.SplitComplementary, new SplitComplementary()},
            { Schemes.DoubleComplementaryRight, new DoubleComplementaryRight()},
            { Schemes.DoubleComplementaryLeft, new DoubleComplementaryLeft()},
            { Schemes.Triadic, new Triadic()},
            { Schemes.TetradicRight, new TetradicRight()},
            { Schemes.Tetradic, new Tetradic()},
            { Schemes.TetradicLeft, new TetradicLeft()},
            { Schemes.AnalogousRight, new AnalogousRight()},
            { Schemes.Analogous, new Analogous()},
            { Schemes.AnalogousLeft, new AnalogousLeft()},
            { Schemes.DiadRight, new DiadRight()},
            { Schemes.DiadLeft, new DiadLeft()}
        };

        private static IEnumerable<Color> Harmonize(Hsl hsl, IEnumerable<int> degrees) {
            return degrees.Select (degree => new Hsl (
                (360 + (hsl.H + 30 * degree)) % 360, hsl.S, hsl.L
            ).ToColor ());
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

        public IEnumerable<KeyValuePair<Schemes, IEnumerable<Color>>> HarmonizeAll(Color color) {
            var hsl = color.ToHsl ();
            return _schemes.Select (scheme =>
             new KeyValuePair<Schemes, IEnumerable<Color>> (scheme.Key, Harmonize (hsl, scheme.Value.Degrees)));
        }

        public IEnumerable<Color> Harmonize(Color color, Schemes scheme) {
            if (!_schemes.ContainsKey (scheme))
                throw new KeyNotFoundException (nameof (scheme)); // useless?
            return Harmonize (color.ToHsl (), _schemes[scheme].Degrees);
        }

        public string GetName(Schemes scheme) {
            return _schemes[scheme].Name;
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
            return Enumerable.Range (0, 7).Select (shade => new Hsl (
                hsl.H, hsl.S, setBetween (hsl.L + getLightness (shade), 0, 100)
            ).ToColor ());
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