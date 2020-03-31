using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver.EditorUnity
{
    public interface ILimitDrawer:ICyberDrawer
    {
         bool IsGoodValue(CyberAttribute attrribute,object val);
    }
}
