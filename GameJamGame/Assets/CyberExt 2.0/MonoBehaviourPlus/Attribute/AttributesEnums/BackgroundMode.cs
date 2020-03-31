using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver.Unity
{
    [Flags]
    public enum BackgroundMode
    {
        None=0,
        Box=1<<0,
        GroupBox= 1<<1,
        HelpBox=1<<2,
    }
  
}
