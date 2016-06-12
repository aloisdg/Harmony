﻿using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace Harmony.Tests {
    [TestFixture]
    public class TestVividMagenta {
        private readonly Color _color = Color.FromArgb(199, 21, 240); // Vivid magenta
        private readonly Harmony _harmony= new Harmony();

        [Test]
        public void TestComplementary() {
            var result = _harmony.Harmonize (_color, "complementary").ToArray ();
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
            var result = _harmony.Harmonize (_color, "doubleComplementaryRight").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 240, 89), result[3]);
        }

        [Test]
        public void TestDoubleComplementaryLeft() {
            var result = _harmony.Harmonize (_color, "doubleComplementaryLeft").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (172, 240, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), result[3]);
        }

        [Test]
        public void TestAnaloguous() {
            var result = _harmony.Harmonize (_color, "analogous").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 172), result[1]);
            Assert.AreEqual (Color.FromArgb (89, 21, 240), result[2]);
        }

        [Test]
        public void TestSplitComplementary() {
            var result = _harmony.Harmonize (_color, "splitComplementary").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (172, 240, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (21, 240, 89), result[2]);
        }

        [Test]
        public void TestTriadic() {
            var result = _harmony.Harmonize (_color, "triadic").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 199, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (21, 240, 199), result[2]);
        }

        [Test]
        public void TestTetradicLeft() { // hexacolor's tetradic
            var result = _harmony.Harmonize (_color, "tetradicLeft").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 199, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 62, 240), result[3]);
        }

        [Test]
        public void TestTetradic() {
            var result = _harmony.Harmonize (_color, "tetradic").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 89, 21), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 172, 240), result[3]);
        }

        [Test]
        public void TestTetradicRight() {
            var result = _harmony.Harmonize (_color, "tetradicRight").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), result[1]);
            Assert.AreEqual (Color.FromArgb (62, 240, 21), result[2]);
            Assert.AreEqual (Color.FromArgb (21, 240, 199), result[3]);
        }

        [Test]
        public void TestDiadRight() {
            var result = _harmony.Harmonize (_color, "diadRight").ToArray ();
            Assert.AreEqual (_color, result[0]);
            Assert.AreEqual (Color.FromArgb (240, 21, 62), result[1]);
        }

        [Test]
        public void TestDiadLeft() {
            var result = _harmony.Harmonize (_color, "diadLeft").ToArray ();
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
        public void TestTemperature() { // darker to lighter
            var main = _harmony.GetTemperature(_color);
            var complementary = _harmony.GetTemperature(Color.FromArgb (62, 240, 21));
            Assert.AreEqual (Temperature.Cool, main);
            Assert.AreEqual (Temperature.Warm, complementary);
        }
    }
}
