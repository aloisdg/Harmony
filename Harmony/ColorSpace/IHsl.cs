namespace Harmony.ColorSpace {
    public interface IHsl : IColorSpace {
        double H { get; set; }
        double S { get; set; }
        double L { get; set; }
    }
}