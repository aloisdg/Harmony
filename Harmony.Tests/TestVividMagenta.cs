using System;
using System.Drawing;
using System.Globalization;
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
            Assert.AreEqual (result[0], _color);
            Assert.AreEqual (result[1], Color.FromArgb (62, 240, 21));
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
        public void TestAnaloguous() {
            var result = _harmony.Harmonize (_color, "analogous").ToArray ();
            Assert.AreEqual (result[0], _color);
            Assert.AreEqual (result[1], Color.FromArgb (240, 21, 172));
            Assert.AreEqual (result[2], Color.FromArgb (89, 21, 240));
        }

        [Test]
        public void TestSplitComplementary() {
            var result = _harmony.Harmonize (_color, "splitComplementary").ToArray ();
            Assert.AreEqual (result[0], _color);
            Assert.AreEqual (result[1], Color.FromArgb (172, 240, 21));
            Assert.AreEqual (result[2], Color.FromArgb (21, 240, 89));
        }

        [Test]
        public void TestTriadic() {
            var result = _harmony.Harmonize (_color, "triadic").ToArray ();
            Assert.AreEqual (result[0], _color);
            Assert.AreEqual (result[1], Color.FromArgb (240, 199, 21));
            Assert.AreEqual (result[2], Color.FromArgb (21, 240, 199));
        }

        [Test]
        public void TestTetradic() {
            var result = _harmony.Harmonize (_color, "tetradicLeft").ToArray ();
            Assert.AreEqual (result[0], _color);
            Assert.AreEqual (result[1], Color.FromArgb (240, 199, 21));
            Assert.AreEqual (result[2], Color.FromArgb (62, 240, 21));
            Assert.AreEqual (result[3], Color.FromArgb (21, 62, 240));
        }

        [Test]
        public void TestMonochromatic() { // darker to lighter
            var result = _harmony.GetMonochromatic(_color).ToArray ();
            Assert.AreEqual (result[0], Color.FromArgb (143, 11, 173));
            Assert.AreEqual (result[1], Color.FromArgb (163, 13, 197));
            Assert.AreEqual (result[2], Color.FromArgb (183, 14, 221));
            Assert.AreEqual (result[3], _color);
            Assert.AreEqual (result[4], Color.FromArgb (205, 45, 242));
            Assert.AreEqual (result[5], Color.FromArgb (210, 69, 243));
            Assert.AreEqual (result[6], Color.FromArgb (216, 93, 245));
        }
    }
}
