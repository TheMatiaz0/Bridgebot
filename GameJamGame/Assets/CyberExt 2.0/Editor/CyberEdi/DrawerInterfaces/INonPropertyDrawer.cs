using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Cyberevolver.EditorUnity
{
    public interface INonPropertDrawer:ICyberDrawer
    {
        void DrawNonProperty(CyberAttribute cyberAttribute, FieldInfo field);
        
    }
}
