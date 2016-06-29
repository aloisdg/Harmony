namespace Harmony.ColorSpace {
    internal class Rgb {
        public double R { get; }
        public double G { get; }
        public double B { get; }

        public Rgb(double r, double g, double b) {
            R = r;
            G = g;
            B = b;
        }

        public Rgb(Rgb color) {
            R = color.R;
            G = color.G;
            B = color.B;
        }
    }
}