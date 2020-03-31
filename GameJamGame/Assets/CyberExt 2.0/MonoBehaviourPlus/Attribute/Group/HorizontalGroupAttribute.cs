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
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method ,AllowMultiple =true)]
    public class HorizontalGroupAttribute : GroupAttribute
    {
        public HorizontalGroupAttribute(string folder) : base(folder)
        {

        }
    }
}
