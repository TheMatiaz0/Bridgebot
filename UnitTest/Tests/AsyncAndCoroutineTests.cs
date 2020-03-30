using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrualityEngine.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace UnitTest
{
    [TestClass]
    public class AsyncAndCoroutineTest
    {

       
      
        [TestMethod]
        public void CoroutineTest()
        {


            TrualityEngine.TheGame.LoadContext();
            uint iterations = 0;
            IEnumerator<ICoroutineable> Work()
            {
                for (int x = 0; x < 20; x++)
                {
                    yield return Async.WaitOneFrame();
                    iterations++;

                }
            }
            SceneRuntime.ChangeScene(new SceneBehaviour()).Yield.Start(Work());
            TestFunctions.Run(10, delegate { });
            Assert.AreEqual(iterations,(uint)9);
           
        }
       
    }
}
