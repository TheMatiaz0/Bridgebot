﻿using System;
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
    /// <summary>
    /// Set background for a field or a method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class BackgroundAttribute : ColorAttribute
    {
     

        public BackgroundAttribute(string rgb) : base(rgb)
        {

        }


        public BackgroundAttribute(AColor color) : base(color)
        {
        }
    }
}
