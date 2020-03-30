using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public static class PointExtension
    {
        public static Resolution ToResolution(this Point p)
        {
            return new Resolution(p);
        }
    }
}
