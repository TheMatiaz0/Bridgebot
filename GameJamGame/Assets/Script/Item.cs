﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
public abstract class Item
{
    public virtual void OnStartSelect() { }
    public virtual void OnSelect() { }
    public virtual void OnEndSelect() { }
    

}