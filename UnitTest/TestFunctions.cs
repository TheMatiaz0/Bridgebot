using TrualityEngine.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TestFunctions
    {
        public static void Run(uint framesQuanity, Action<uint, TimeSpan> action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (uint x = 0; x < framesQuanity; x++)
            {
                TimeSpan time;

                GameManager.Update(time=stopwatch.Elapsed);
                action?.Invoke(framesQuanity, time);
            }
            stopwatch.Stop();
        }
        public static SceneRuntime PrepareToUpdate(SceneBehaviour prefix =null)
        {
            TrualityEngine.TheGame.LoadContext();
           return  SceneRuntime.ChangeScene(prefix??new SceneBehaviour());
        }

    }
}
