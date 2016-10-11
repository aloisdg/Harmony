using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Harmony.ColorSpace;
using NUnit.Framework;

namespace Harmony.Tests {
    [TestFixture]
    public class TestVividMagenta {
        private static readonly Color Color = Color.FromArgb(199, 21, 240); // Vivid magenta
        private static readonly Harmony Harmony= new Harmony(Color);

        [Test]
        public void TestComplementary() {
            Assert.AreEqual (Color, Harmony.Complementary[0]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), Harmony.Complementary[1]);
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
            Assert.AreEqual (Color, Harmony.DoubleComplementaryRight[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), Harmony.DoubleComplementaryRight[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), Harmony.DoubleComplementaryRight[2]);
            Assert.AreEqual (Color.FromArgb (21, 240, 89), Harmony.DoubleComplementaryRight[3]);
        }

        [Test]
        public void TestDoubleComplementaryLeft() {
            Assert.AreEqual (Color, Harmony.DoubleComplementaryLeft[0]);
            Assert.AreEqual (Color.FromArgb (172, 240, 21), Harmony.DoubleComplementaryLeft[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), Harmony.DoubleComplementaryLeft[2]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), Harmony.DoubleComplementaryLeft[3]);
        }

        [Test]
        public void TestAnaloguousLeft() {
            Assert.AreEqual (Color, Harmony.AnalogousLeft[0]);
            Assert.AreEqual (Color.FromArgb (21, 62, 240), Harmony.AnalogousLeft[1]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), Harmony.AnalogousLeft[2]);
        }

        [Test]
        public void TestAnaloguousRight() {
            Assert.AreEqual (Color, Harmony.AnalogousRight[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), Harmony.AnalogousRight[1]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), Harmony.AnalogousRight[2]);
        }

        [Test]
        public void TestAnaloguous() {
            Assert.AreEqual (Color, Harmony.Analogous[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), Harmony.Analogous[1]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), Harmony.Analogous[2]);
        }

        [Test]
        public void TestSplitComplementary() {
            Assert.AreEqual (Color, Harmony.SplitComplementary[0]);
            Assert.AreEqual (Color.FromArgb (172, 240, 21), Harmony.SplitComplementary[1]);
            Assert.AreEqual (Color.FromArgb (21, 240, 89), Harmony.SplitComplementary[2]);
        }

        [Test]
        public void TestTriadic() {
            Assert.AreEqual (Color, Harmony.Triadic[0]);
            Assert.AreEqual (Color.FromArgb (240, 199, 21), Harmony.Triadic[1]);
            Assert.AreEqual (Color.FromArgb (21, 240, 199), Harmony.Triadic[2]);
        }

        [Test]
        public void TestTetradicLeft() { // hexacolor's tetradic
            Assert.AreEqual (Color, Harmony.TetradicLeft[0]);
            Assert.AreEqual (Color.FromArgb (240, 199, 21), Harmony.TetradicLeft[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), Harmony.TetradicLeft[2]);
            Assert.AreEqual (Color.FromArgb (21, 62, 240), Harmony.TetradicLeft[3]);
        }

        [Test]
        public void TestTetradic() {
            Assert.AreEqual (Color, Harmony.Tetradic[0]);
            Assert.AreEqual (Color.FromArgb (240, 89, 21), Harmony.Tetradic[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), Harmony.Tetradic[2]);
            Assert.AreEqual (Color.FromArgb (21, 172, 240), Harmony.Tetradic[3]);
        }

        [Test]
        public void TestTetradicRight() {
            Assert.AreEqual (Color, Harmony.TetradicRight[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), Harmony.TetradicRight[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), Harmony.TetradicRight[2]);
            Assert.AreEqual (Color.FromArgb (21, 240, 199), Harmony.TetradicRight[3]);
        }

        [Test]
        public void TestDiadRight() {
            Assert.AreEqual (Color, Harmony.DiadRight[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), Harmony.DiadRight[1]);
        }

        [Test]
        public void TestDiadLeft() {
            Assert.AreEqual (Color, Harmony.DiadLeft[0]);
            Assert.AreEqual (Color.FromArgb (21, 62, 240), Harmony.DiadLeft[1]);
        }

        [Test]
        public void TestMonochromatic() { // darker to lighter
            Assert.AreEqual (Color.FromArgb (143, 11, 173), Harmony.Monochromatic[0]);
            Assert.AreEqual (Color.FromArgb (163, 13, 197), Harmony.Monochromatic[1]);
            Assert.AreEqual (Color.FromArgb (183, 14, 221), Harmony.Monochromatic[2]);
            Assert.AreEqual (Color, Harmony.Monochromatic[3]);
            Assert.AreEqual (Color.FromArgb (205, 45, 242), Harmony.Monochromatic[4]);
            Assert.AreEqual (Color.FromArgb (210, 69, 243), Harmony.Monochromatic[5]);
            Assert.AreEqual (Color.FromArgb (216, 93, 245), Harmony.Monochromatic[6]);
        }

        [Test]
        public void TestTemperature() {
            Assert.AreEqual (Temperature.Cool, Harmony.Temperature);
            var complementary = new Harmony (Color.FromArgb (62, 240, 21)).Temperature;
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
        public short TestTemperatureAsNumber(int hue) { // from -100 to 100 // Celsius?
            return new Harmony (ToColor (new Hsl { H = hue, L = 50, S = 50 })).TemperatureDegree;
        }

        internal static Color ToColor(Hsl hsl) {
            Func<double, int> round = x => (int) Math.Round (x);
            var rgb = hsl.ToRgb ();
            return Color.FromArgb (round (rgb.R), round (rgb.G), round (rgb.B));
        }
    }
}
