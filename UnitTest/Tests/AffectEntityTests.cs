using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using TrualityEngine.Core;
namespace UnitTest
{
    [TestClass]
    public class AffectEntityTests
    {
        [TestMethod]
        public void RotateTest()
        {
            var a = new SpriteEntity();
            var child = a.AddChild(new SpriteEntity());
            a.SelfRotate += Rotation.FromDegrees(20);
            Assert.AreEqual(a.FullRotate, child.FullRotate);
            child.RenderRotation += Rotation.FromDegrees(20);
            Assert.AreEqual(40, child.FullRotate.Angle);
            Assert.AreEqual(20, child.RenderRotation.Angle);
            
        }
    }
}
