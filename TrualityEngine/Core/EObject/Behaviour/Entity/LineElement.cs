using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{
    public class LineElement
    {
      
       
        public Vect2 End { get; set; }
        public int Scale { get; set; } = 1;
        public LineElement(Vect2 end)
        {
            
            End = end;
        }
        public LineElement(float x, float y):this(new Vect2(x,y))
        {
           
        }
       
      
    }
}
