using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{

    public class GroupLineEntity:RenderEntity
    {
        public List<LineElement> Lines { get; private set; }         
     
        public GroupLineEntity()
        {
            Lines = new List<LineElement>();
        }
     
        protected internal override void IfDraw(FixedBatch fixedBatch)
        {
            base.IfDraw(fixedBatch);
            LineElement last = new LineElement(this.Pos);
            foreach (LineElement line in Lines)
            {

                fixedBatch.DrawLine(this.Pos + last.End, this.Pos + line.End, RenderColor, line.Scale * (int)((FullScale.X + FullScale.Y) / 2));;
                last = line;
            }
                
        }


    }
}
