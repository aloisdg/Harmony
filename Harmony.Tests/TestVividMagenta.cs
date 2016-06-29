using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Harmony.ColorSpace;
using NUnit.Framework;

namespace Harmony.Tests {
    [TestFixture]
    public class TestVividMagenta {
        private readonly Color _color = Color.FromArgb(199, 21, 240); // Vivid magenta
        private readonly Harmony _harmony= new Harmony();

        [Test]
        public void TestComplementary() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.Complementary).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[1]);
        }

        //todo: Handle from shades of grey (from white to black)
        // http://www.colorhexa.com/222222 handle as error or return empty?
        //[Test]
        //public void TestComplementary2() {
        //    var result = _harmony.Harmonize (Color.FromArgb (255, 255, 255), "complementary").ToArray ();
        //    Assert.AreEqual (result[0], Color.FromArgb (255, 255, 255));
        //    //Assert.AreEqual (result[1], Color.FromArgb (0, 0, 0));
        //}

        [Test]
        public void TestDoubleComplementaryRight() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.DoubleComplementaryRight).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 240, 89), result[3]);
        }

        [Test]
        public void TestDoubleComplementaryLeft() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.DoubleComplementaryLeft).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (172, 240, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), result[3]);
        }

        [Test]
        public void TestAnaloguousLeft() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.AnalogousLeft).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (21, 62, 240), result[1]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), result[2]);
        }

        [Test]
        public void TestAnaloguousRight() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.AnalogousRight).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), result[1]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), result[2]);
        }

        [Test]
        public void TestAnaloguous() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.Analogous).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), result[1]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), result[2]);
        }

        [Test]
        public void TestSplitComplementary() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.SplitComplementary).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (172, 240, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (21, 240, 89), result[2]);
        }

        [Test]
        public void TestTriadic() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.Triadic).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 199, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (21, 240, 199), result[2]);
        }

        [Test]
        public void TestTetradicLeft() { // hexacolor's tetradic
            var result = _harmony.Harmonize (_color, Harmony.Schemes.TetradicLeft).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 199, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 62, 240), result[3]);
        }

        [Test]
        public void TestTetradic() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.Tetradic).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 89, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 172, 240), result[3]);
        }

        [Test]
        public void TestTetradicRight() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.TetradicRight).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 240, 199), result[3]);
        }

        [Test]
        public void TestDiadRight() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.DiadRight).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), result[1]);
        }

        [Test]
        public void TestDiadLeft() {
            var result = _harmony.Harmonize (_color, Harmony.Schemes.DiadLeft).ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (21, 62, 240), result[1]);
        }

        [Test]
        public void TestMonochromatic() { // darker to lighter
            var result = _harmony.GetMonochromatic (_color).ToArray ();
            Assert.AreEqual (Color.FromArgb (143, 11, 173), result[0]);
            Assert.AreEqual (Color.FromArgb (163, 13, 197), result[1]);
            Assert.AreEqual (Color.FromArgb (183, 14, 221), result[2]);
            Assert.AreEqual (_color, result[3]);
            Assert.AreEqual (Color.FromArgb (205, 45, 242), result[4]);
            Assert.AreEqual (Color.FromArgb (210, 69, 243), result[5]);
            Assert.AreEqual (Color.FromArgb (216, 93, 245), result[6]);
        }

        [Test]
        public void TestTemperature() {
            var main = _harmony.GetTemperature (_color);
            var complementary = _harmony.GetTemperature (Color.FromArgb (62, 240, 21));
            Assert.AreEqual (Temperature.Cool, main);
            Assert.AreEqual (Temperature.Warm, complementary);
        }

        [TestCase (330, ExpectedResult = 0)]
        [TestCase (360, ExpectedResult = 33)]
        [TestCase (0, ExpectedResult = 33)]
        [TestCase (30, ExpectedResult = 66)]
        [TestCase (60, ExpectedResult = 100)]
        [TestCase (90, ExpectedResult = 66)]
        [TestCase (120, ExpectedResult = 33)]
        [TestCase (150, ExpectedResult = 0)]
        [TestCase (180, ExpectedResult = -33)]
        [TestCase (210, ExpectedResult = -66)]
        [TestCase (240, ExpectedResult = -100)]
        [TestCase (270, ExpectedResult = -66)]
        [TestCase (300, ExpectedResult = -33)]
        [TestCase (400, ExpectedResult = 77)]
        [TestCase (40, ExpectedResult = 77)]
        public short TestTemperatureAsNumber(int hue) { // from -100 to 100
            var celsius = _harmony.GetTemperatureAsNumber (ToColor (new Hsl (hue, 50, 50)));
            return celsius;
        }

        internal static Color ToColor(Hsl hsl) {
            Func<double, int> round = x => (int) Math.Round (x);
            var rgb = hsl.ToRgb ();
            return Color.FromArgb (round (rgb.R), round (rgb.G), round (rgb.B));
        }
    }
}
