﻿using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace  Cyberevolver.EditorUnity
{ 
   public interface IGroupShowWhenDrawer:ICyberDrawer
    {
        bool CanDraw(CyberAttribute cyberAttrribute);
        
    }
}
