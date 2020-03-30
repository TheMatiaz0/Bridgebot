using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrualityEngine.Core;

using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class ComponentTest
    {


        class TestComponent:Component<Entity>
        {
            public static int Updater { get;private  set; } = 0;
            public TestComponent():base(multipleLock: true)
            {

            }
            public override void Update()
            {
                base.Update();
                Updater++;
                
            }
        }
        [TestMethod]
        public void ComponentWorkTest()
        {
            TestFunctions.PrepareToUpdate();
            Entity entity = new Entity();
            entity.ComponentManager.Add<TestComponent>();
            TestFunctions.Run(3, null);
            Assert.IsTrue(TestComponent.Updater>=3);
        }
     
        [TestMethod]
        public void ComponentAddingTest()
        {
            TestFunctions.PrepareToUpdate();
            Entity entity = new Entity();
            TestComponent component = new TestComponent();
            entity.ComponentManager.Add(component);
            try
            {
                entity.ComponentManager.Add(component);
                Assert.Fail();
            }
            catch (ArgumentException) { }
           
            Assert.AreEqual(component, entity.ComponentManager.Get<TestComponent>());
            entity.ComponentManager.Remove(component);
            Assert.AreEqual(0, entity.ComponentManager.GetAll().Count);
            try
            {
                entity.ComponentManager.Add(component);
                Assert.Fail();
            }
            catch (ArgumentException) { }
            Assert.AreEqual(0, entity.ComponentManager.GetAll().Count);
            Assert.AreEqual(null, entity.ComponentManager.Get<TestComponent>());
        }
        [TestMethod]
        public void ComponentEventTest()
        {
            TestFunctions.PrepareToUpdate();
            Entity entity = new Entity();
            bool isKilled = false;
            bool isStart = false;
            entity.ComponentManager.Add<TestComponent>().OnBeforeKilling.Value += (s, e)
                  =>
              {
                  isKilled = true;
              };
            entity.ComponentManager.Get<TestComponent>().OnStart.Value += (s, e)
                  =>
              {
                  isStart = true;
              };
            TestFunctions.Run(1, null);
            entity.Kill();
            Assert.IsTrue(isKilled);
            Assert.IsTrue(isStart);

        }
    }
}
