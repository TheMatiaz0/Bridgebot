using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrualityEngine.Core;
using Microsoft.Xna.Framework;
namespace UnitTest
{
    [TestClass]
    public class LineTest
    {
        [TestMethod]
        public void LineTest1()
        {
            
          
            Assert.IsTrue(new Line(1, 0, 10, 0).Intersects(new Rect(0, 0, 2, 2)));
            Assert.IsFalse(new Line(-14, 0, -2, 0).Intersects(new Rect(0, 0, 2, 2)));
            Assert.IsTrue(new Line(1, 1, 2, 1).Intersects(new Rect(0, 0, 1, 1)));

            
        }
        [TestMethod]
        public void LineTest2()
        {
            Assert.IsTrue(new Line(0, 0, 1, 2).Intersects(new Line(1, 2, 0, 0)));   
            Assert.IsFalse(new Line(-2, -1, -1, -2).Intersects(new Line(1, 2, 1,2)));
            
        }

       
    }
}
