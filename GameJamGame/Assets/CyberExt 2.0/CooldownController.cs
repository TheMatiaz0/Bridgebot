using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
namespace Cyberevolver.Unity
{
    public class CooldownController
    {
        private readonly AsyncStoper stoper;


        public CooldownController(MonoBehaviour main, TimeSpan time, bool cooldownOnStart = false)
        {

            stoper = new AsyncStoper(main, time);
            if (cooldownOnStart)
                Reset();
        }
        public CooldownController(MonoBehaviour main, float seconds, bool cooldownOnStart = false) : this(main, TimeSpan.FromSeconds(seconds), cooldownOnStart)
        {

        }
        public static CooldownController MakeImmediately(MonoBehaviour main)
        {
            return new CooldownController(main, TimeSpan.Zero);
        }
        /// <summary>
        /// Cheks if cooldown pass and reset it if it's true.
        /// </summary>
        /// <returns></returns>
        public bool Try()
        {
            if (stoper.MaxTime == TimeSpan.Zero)
                return true;
            if (stoper.IsStoperRunning == false)
            {
                stoper.Start();
                return true;
            }
            return false;
        }
        public void Reset()
        {
            stoper.Start();
        }
        /// <summary>
        /// Checks if cooldown pass, but don't reset it.
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            return stoper.IsStoperRunning;
        }



    }
}
