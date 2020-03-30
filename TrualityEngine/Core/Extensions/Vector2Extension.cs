using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TrualityEngine.Core
{
    public static class Vector2Extension
    {
      
     
      
        public static Vector2 GetNormalized(this Vector2 vect)
        {
            vect.Normalize();
            return vect;
        }
       
             
        
    }
}
