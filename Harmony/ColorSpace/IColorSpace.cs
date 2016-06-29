namespace Harmony.ColorSpace {
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
    }
}
