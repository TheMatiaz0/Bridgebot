using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrualityEngine.Core;

namespace UnitTest
{
    [TestClass]
    public class ActivatedTest
    {
        class MyC : Component<Entity>
        {

        }
        [TestMethod]
        public void ActivatedTest1()
        {
            Entity entity = new Entity();
            Entity child = new Entity();
            child.ComponentManager.Add<MyC>();
            entity.AddChild(child);
            int eventDone = 0;
            void Event(object sender, EventArgs args)
            {
                eventDone++;
            }
            entity.OnActivateChange.Value += Event;
            child.OnActivateChange.Value += Event;
            child.ComponentManager.Get<MyC>().OnActivateChange.Value += Event;                 
            entity.IsActiveSelf = false;
            Assert.IsFalse(child.IsActive);
            Assert.IsFalse(child.ComponentManager.Get<MyC>().IsActive);
            entity.IsActiveSelf = true;        
            Assert.AreEqual(eventDone, 6);
            child.IsActiveSelf = false;
            Assert.AreEqual(eventDone, 8);



        }
    }
}
