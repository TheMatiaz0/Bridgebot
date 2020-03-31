﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public static class UnityEventExtension 
{
    public static IEnumerable<UnityEngine.Object> GetAllTargets(this UnityEventBase ev)
    {
        int len = ev.GetPersistentEventCount();
        for(int i=0;i<len;i++)
        {
            yield return ev.GetPersistentTarget(i);
        }
    }
}