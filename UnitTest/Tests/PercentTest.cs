using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrualityEngine.Core;
namespace UnitTest
{
    [TestClass]
    public class PercentTest
    {
        [TestMethod]
        public void PercentTest1()
        {
            Assert.IsTrue(0.5 == Percent.FromPercent(50));
            Assert.IsTrue(new Percent(0.5) == Percent.FromPercent(50));
            Assert.IsTrue(0.99f + Percent.FromDecimal(0.2) == Percent.Full);
            Assert.IsTrue(0.55f - Percent.FromDecimal(0.7) == Percent.Zero);
            Assert.IsTrue(Percent.Half < Percent.Full);
            Assert.IsTrue(Percent.Half / 2.0 == 0.25);



        }
    }
}
