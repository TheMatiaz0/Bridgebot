using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



[StaticMethodContainer]
public static class GameObjectHelper
{
    public static void Destroy(UnityEngine.Object obj)
        => UnityEngine.Object.Destroy(obj);
}

