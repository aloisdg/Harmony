using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmony {
    internal static class HslConverter {
        internal static void ToColorSpace(IRgb color, IHsl item) {
            // TODO Losing precision
            var msColor = Color.FromArgb ((int) color.R, (int) color.G, (int) color.B);
            item.H = msColor.GetHue ();
            item.S = msColor.GetSaturation () * 100.0;
            item.L = msColor.GetBrightness () * 100.0;
        }

        internal static IRgb ToColor(IHsl item) {
            var rangedH = item.H / 360.0;
            var r = 0.0;
            var g = 0.0;
            var b = 0.0;
            var s = item.S / 100.0;
            var l = item.L / 100.0;

            if (l.BasicallyEqualTo (0))
                return new Rgb {
                    R = 255.0 * r,
                    G = 255.0 * g,
                    B = 255.0 * b
                };
            if (s.BasicallyEqualTo (0))
                r = g = b = l;
            else {
                var temp2 = l < 0.5 ? l * (1.0 + s) : l + s - l * s;
                var temp1 = 2.0 * l - temp2;

                r = GetColorComponent (temp1, temp2, rangedH + 1.0 / 3.0);
                g = GetColorComponent (temp1, temp2, rangedH);
                b = GetColorComponent (temp1, temp2, rangedH - 1.0 / 3.0);
            }
            return new Rgb {
                R = 255.0 * r,
                G = 255.0 * g,
                B = 255.0 * b
            };
        }

        private static double GetColorComponent(double temp1, double temp2, double temp3) {
            temp3 = MoveIntoRange (temp3);
            if (temp3 < 1.0 / 6.0)
                return temp1 + (temp2 - temp1) * 6.0 * temp3;

            if (temp3 < 0.5)
                return temp2;

            if (temp3 < 2.0 / 3.0)
                return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);

            return temp1;
        }

        private static double MoveIntoRange(double temp3) {
            if (temp3 < 0.0)
                return temp3 + 1.0;
            if (temp3 > 1.0)
                return temp3 - 1.0;
            return temp3;
        }
    }

    internal static class DoubleExtension {
        private const double DefaultPrecision = .0001;

        internal static bool BasicallyEqualTo(this double a, double b) {
            return a.BasicallyEqualTo (b, DefaultPrecision);
        }

        internal static bool BasicallyEqualTo(this double a, double b, double precision) {
            return Math.Abs (a - b) <= precision;
        }
    }

    public interface IHsl : IColorSpace {
        double H { get; set; }
        double S { get; set; }
        double L { get; set; }

    }

    public class Hsl : ColorSpace, IHsl {
        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }


        public override void Initialize(IRgb color) {
            HslConverter.ToColorSpace (color, this);
        }

        public override IRgb ToRgb() {
            return HslConverter.ToColor (this);
        }
    }

    public class Rgb : ColorSpace, IRgb {

        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }


        public override void Initialize(IRgb color) {
            RgbConverter.ToColorSpace (color, this);
        }

        public override IRgb ToRgb() {
            return RgbConverter.ToColor (this);
        }
    }

    internal static class RgbConverter {
        internal static void ToColorSpace(IRgb color, IRgb item) {
            item.R = color.R;
            item.G = color.G;
            item.B = color.B;
        }

        internal static IRgb ToColor(IRgb item) {
            return item;
        }
    }

    public interface IRgb : IColorSpace {
        double R { get; set; }
        double G { get; set; }
        double B { get; set; }
    }

    public delegate double ComparisonAlgorithm(IColorSpace a, IColorSpace b);

    /// <summary>
    /// Defines the public methods for all color spaces
    /// </summary>
    public interface IColorSpace {
        /// <summary>
        /// Initialize settings from an Rgb object
        /// </summary>
        /// <param name="color"></param>
        void Initialize(IRgb color);

        /// <summary>
        /// Convert the color space to Rgb, you should probably using the "To" method instead. Need to figure out a way to "hide" or otherwise remove this method from the public interface.
        /// </summary>
        /// <returns></returns>
        IRgb ToRgb();

        /// <summary>
        /// Convert any IColorSpace to any other IColorSpace.
        /// </summary>
        /// <typeparam name="T">IColorSpace type to convert to</typeparam>
        /// <returns></returns>
        T To<T>() where T : IColorSpace, new();

        /// <summary>
        /// Determine how close two IColorSpaces are to each other using a passed in algorithm
        /// </summary>
        /// <param name="compareToValue">Other IColorSpace to compare to</param>
        /// <param name="comparer">Algorithm to use for comparison</param>
        /// <returns>Distance in 3d space as double</returns>
        double Compare(IColorSpace compareToValue, IColorSpaceComparison comparer);
    }

    /// <summary>
    /// Abstract ColorSpace class, defines the To method that converts between any IColorSpace.
    /// </summary>
    public abstract class ColorSpace : IColorSpace {
        public abstract void Initialize(IRgb color);
        public abstract IRgb ToRgb();

        /// <summary>
        /// Convienience method for comparing any IColorSpace
        /// </summary>
        /// <param name="compareToValue"></param>
        /// <param name="comparer"></param>
        /// <returns>Single number representing the difference between two colors</returns>
        public double Compare(IColorSpace compareToValue, IColorSpaceComparison comparer) {
            return comparer.Compare (this, compareToValue);
        }

        /// <summary>
        /// Convert any IColorSpace to any other IColorSpace
        /// </summary>
        /// <typeparam name="T">Must implement IColorSpace, new()</typeparam>
        /// <returns></returns>
        public T To<T>() where T : IColorSpace, new() {
            if (typeof (T) == GetType ()) {
                return (T) MemberwiseClone ();
            }

            var newColorSpace = new T ();
            newColorSpace.Initialize (ToRgb ());

            return newColorSpace;
        }
    }

    /// <summary>
    /// Defines how comparison methods may be called
    /// </summary>
    public interface IColorSpaceComparison {
        /// <summary>
        /// Returns the difference between two colors given based on the specified defined in the called class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Score based on similarity, the lower the score the closer the colors</returns>
        double Compare(IColorSpace a, IColorSpace b);
    }
}
