using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using TrualityEngine.Core;
using System.Runtime.Serialization;

namespace TrualityEngine.Core
{
    /// <summary>
    /// This exception will be throwed when object spawn on wrong context.
    /// </summary>
    public class WrongCreatingContextException : Exception
    {
        public WrongCreatingContextException()
        {
        }

        public WrongCreatingContextException(string message) : base(message)
        {
        }




    }

}
