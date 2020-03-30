using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Use it on static method OR instance scene method.
    /// It works only in debug mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DebugBindAttribute:Attribute
    {


        public Keys Key { get; }
        public DebugBindAttribute(Keys keys)
        {
            Key = keys;
        }


    }
}
