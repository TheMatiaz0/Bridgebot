using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TrualityEngine.Core
{

    /// <summary>
    /// It just never colliding.
    /// </summary>
    public class NeverColliding : ICollisionInfo
    {
        public bool Contains(Vect2 pos)
        {
            return false;
        }

        public bool Intersects(ICollisionInfo collisionInfo)
        {
            return false;
        }
    }
    public class AlwaysColliding : ICollisionInfo
    {
        public bool Contains(Vect2 pos)
        {
            return true;
        }

        public bool Intersects(ICollisionInfo collisionInfo)
        {
            return true;
        }
    }
}
