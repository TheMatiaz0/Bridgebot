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
    /// Changes a prefix GUI, for examples makes it bold.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method
        ,AllowMultiple =false)]
    public class CustomGuiAttribute : FormaterAttribute
    {
        public CustomGuiAttribute(string label, string rgb, UISize size = UISize.Default, int fontSize = 0) : base(label, rgb, size, fontSize)
        {
        }

        public CustomGuiAttribute(string label, FontStyle fontStyle = FontStyle.Normal, AColor color = AColor.None, UISize size = UISize.Default, int fontSize = 0) : base(label, fontStyle, color, size, fontSize)
        {
        }

        public CustomGuiAttribute(string label, FontStyle fontStyle, string rgb, UISize size = UISize.Default, int fontSize = 0) : base(label, fontStyle, rgb, size, fontSize)
        {
        }
    }
}

