using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrualityEngine.Core;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class SceneTest
    {
        [TestMethod]
        public  void SceneTest1()
        {
            TestFunctions.PrepareToUpdate();
          
            var entity= new Entity();
          
            SceneRuntime.ChangeScene(new SceneBehaviour());
          
            
            Assert.IsFalse( Entity.GetEntities().Any(item=>item==entity));

        }
        class MyScene: SceneBehaviour
        {
            public static int X { get; private set; } = 0;
            public static bool Close { get; private set; } = false;
            public override void Update()
            {
                base.Update();
                X++;


            }

            public override void AfterClosingScene()
            {
                base.AfterClosingScene();
                Close = true;
            }

        }
        [TestMethod]
        public void SceneTest2()
        {
            TestFunctions.PrepareToUpdate(new MyScene());
            TestFunctions.Run(3, null);
            Assert.IsTrue(MyScene.X >= 3);
            SceneRuntime.ChangeScene(new SceneBehaviour());
            Assert.IsTrue(MyScene.Close);
            

        }
    }
}
