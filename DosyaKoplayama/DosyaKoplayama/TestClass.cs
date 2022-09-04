using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace DosyaKoplayama
{
    [TestFixture]
    class TestClass
    {
        [Test]
        public void TestCopiedSuccefully()
        {

            Assert.AreEqual(true, true);
        }

        [Test]
        public void TestAlwaysFalse()
        {

            Assert.AreEqual(true, false);
        }

        [Test]
        public void TestBackColor()
        {
            Assert.AreEqual(Color.Red, Program.form.ChangeBackgroundColor("kırmızı"));
            Assert.AreEqual(Color.Red, Program.form.ChangeBackgroundColor("yeşil"));
            Assert.AreEqual(Color.Red, Program.form.ChangeBackgroundColor("mavi"));

        }
    }
}
