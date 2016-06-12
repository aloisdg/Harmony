using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ColorMine.ColorSpaces;

// inspiration
// https://github.com/skratchdot/color-harmony

// color picker
// http://www.colorhexa.com/c715f0 // best by far
// https://duckduckgo.com/?q=color+picker+%23c715f0&ia=colorpicker
// http://www.sessions.edu/color-calculator

namespace Harmony {
    public class Harmony {
        // http://galacticmilk.com/sphere/#config=%7B%22hsl%22:%7B%22H%22:360,%22S%22:100,%22L%22:0%7D,%22theme%22:%22dark%22,%22quantize%22:0,%22harmony%22:%22Complementary%22,%22vision%22:%22Normal%22,%22space%22:%22RYB%22,%22top%22:62,%22left%22:50,%22diameter%22:500,%22scale%22:1,%22dot%22:false%7D
        //private readonly Dictionary<string, int[]> _harmonies = new Dictionary<string, int[]>
        //{
        //    { "complementary", new[] {0, 180}},
        //    { "splitComplementary", new[] {0, 150, 320}},
        //    { "splitComplementaryCW", new[] {0, 150, 300}},
        //    { "splitComplementaryCCW", new[] {0, 60, 210}},
        //    { "triadic", new[] {0, 120, 240}},
        //    { "clash", new[] {0, 90, 270}},
        //    { "tetradic", new[] {0, 90, 180, 270}},
        //    { "fourToneCW", new[] {0, 60, 180, 240}},
        //    { "fourToneCCW", new[] {0, 120, 180, 300}},
        //    { "fiveToneA", new[] {0, 115, 155, 205, 245}},
        //    { "fiveToneB", new[] {0, 40, 90, 130, 245}},
        //    { "fiveToneC", new[] {0, 50, 90, 205, 320}},
        //    { "fiveToneD", new[] {0, 40, 155, 270, 310}},
        //    { "fiveToneE", new[] {0, 115, 230, 270, 320}},
        //    { "sixToneCW", new[] {0, 30, 120, 150, 240, 270}},
        //    { "sixToneCCW", new[] {0, 90, 120, 210, 240, 330}},
        //    { "neutral", new[] {0, 15, 30, 45, 60, 75}},
        //    {"analogous", new[] {0, 30, 60, 90, 120, 150}}
        //};

        // http://www.tigercolor.com/color-lab/color-theory/color-harmonies.htm
        // http://www.sensationalcolor.com/understanding-color/theory/color-relationships-creating-color-harmony-1849
        // http://www.chainstyle.com/tutorials/colscheme.html
        // http://www.easyrgb.com/index.php?X=WEEL
        private readonly Dictionary<string, int[]> _harmonies = new Dictionary<string, int[]> {
            { "complementary", new[] { 0, 6 }},
            { "splitComplementary", new[] { 0, 5, 7 }},
            //{ "doubleComplementaryRight", new[] { 0, 1, 6, 7 }},
            //{ "doubleComplementaryLeft", new[] { 0, 5, 6, 11 }},
            { "triadic", new[] { 0, 4, 8 }},
            //{ "tetradicRight", new[] { 0, 2, 6, 8 }},
            //{ "tetradic", new[] { 0, 3, 6, 9 }}, // also called square
            { "tetradicLeft", new[] { 0, 4, 6, 10 }},
            { "analogous", new[] { 0, 1, 11 }},
            //{ "diadRight", new[] { 0, 2 }},
            //{ "diadLeft", new[] { 0, 10 }}
        };

        private static IEnumerable<Color> Harmonize(Color color, IEnumerable<int> degrees) {
            var hsl = color.ToHsl ();
            return degrees.Select (degree => new Hsl {
                H = (360 + (hsl.H + 30 * degree)) % 360, // (hsl.H + degree / 360d),// % 1,
                S = hsl.S,
                L = hsl.L
            }.ToColor ());
        }

        //private static IEnumerable<Color> ScaleTo(Color color, double size, dynamic scale) {
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
        //    if (_harmonies.ContainsKey (name)) //maybe useless
        //        throw new ArgumentException ("An item with the same key has already been added.");
        //    _harmonies.Add (name, degrees);
        //}

        public IEnumerable<KeyValuePair<string, IEnumerable<Color>>> HarmonizeAll(Color color) {
            return _harmonies.Select (harmony =>
             new KeyValuePair<string, IEnumerable<Color>> (harmony.Key, Harmonize (color, harmony.Value)));
        }

        public IEnumerable<Color> Harmonize(Color color, string harmony) {
            if (!_harmonies.ContainsKey (harmony))
                throw new KeyNotFoundException (nameof (harmony)); // useless?
            return Harmonize (color, _harmonies[harmony]);
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
    }
}