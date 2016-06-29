namespace Harmony.ColorSpace {
    /// <summary>
    /// Abstract ColorSpace class, defines the To method that converts between any IColorSpace.
    /// </summary>
    public abstract class AColorSpace : IColorSpace {
        public abstract void Initialize(IRgb color);
        public abstract IRgb ToRgb();

        /// <summary>
        /// Convert any IColorSpace to any other IColorSpace
        /// </summary>
        /// <typeparam name="T">Must implement IColorSpace, new()</typeparam>
        /// <returns></returns>
        public T To<T>() where T : IColorSpace, new() {
            if (typeof (T) == GetType ())
                return (T) MemberwiseClone ();

            var newColorSpace = new T ();
            newColorSpace.Initialize (ToRgb ());
            return newColorSpace;
        }
    }
}