using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cyberevolver.Unity;
using Cyberevolver;
using System;
using UnityEngine.Events;
using System.Reflection;

namespace XX
{


    [ShowCyberInspector]
    public class Example : MonoBehaviourPlus
    {
        private void Start()
        {
           
               

        }
       
        private void TestIt()
        {
            Time.timeScale = 0;
            InvokeRepeating((m) => Debug.Log($"repeat {Time.time}"), TimeSpan.FromSeconds(1), 5)
                .SetScaled(false)
                .SetOnEnd((m) => Debug.Log($"End {Time.time}"),TimeSpan.FromSeconds(3));
        }
    }

}


public class Mother : MonoBehaviourPlus
{
  
   [Auto]
   public Rigidbody2D Rigidbody { get; private set; }
}

