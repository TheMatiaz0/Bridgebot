
using System;
using TrualityEngine.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace UnitTest
{
    [TestClass]
    public class ColliderTest
    {
        [TestMethod]
        public void ColliderTest1()
        {

            ColliderEntity entity = new ColliderEntity() { Scale = new Vector2(32, 32) };
            ColliderEntity entity1 = new ColliderEntity() { Scale = new Vector2(32, 32) };
            ColliderEntity entity2 = new ColliderEntity() { Scale = new Vector2(1, 1) };
            ColliderEntity entity3 = new ColliderEntity() { Scale = new Vector2(32, 32), Pos = new Vector2(31, 31) };
            ColliderEntity entity4 = new ColliderEntity() { Scale = new Vector2(32, 32), Pos = new Vector2(80, 80) };
            Assert.IsTrue(entity.ColliderManager.IsCollision(entity1));
            Assert.IsTrue(entity.ColliderManager.IsCollision(entity2));
            Assert.IsTrue(entity.ColliderManager.IsCollision(entity3));
            Assert.IsTrue(entity.ColliderManager.IsCollision(entity4) == false);

        }
    }
}
