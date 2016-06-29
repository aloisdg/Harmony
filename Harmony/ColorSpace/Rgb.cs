namespace Harmony.ColorSpace {
    internal class Rgb : AColorSpace, IRgb {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        public override void Initialize(IRgb color) {
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public override IRgb ToRgb() {
            return this;
        }
    }
}