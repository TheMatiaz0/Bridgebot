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
   
    public abstract class GroupModiferAttribute : CyberAttribute
    {
   
        public string Folder { get; }
        public GroupModiferAttribute(string folder)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
        }

    }
}
