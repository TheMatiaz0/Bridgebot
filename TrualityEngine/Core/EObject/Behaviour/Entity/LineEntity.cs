using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Represent entity, that can drawe line on screen and can detect collison on line
    /// </summary>
    public class LineEntity:RenderEntity
    {
        /// <summary>
        /// Start that it is drawing, is equal Start+Pos
        /// </summary>
       
        public Vect2 SubsidiaryStart { get; set; }
        /// <summary>
        /// End that it is drawing, is equal End+Pos
        /// </summary>
       
        public Vect2 SubsidiaryEnd { get; set; }
        /// <summary>
        /// Realy drawing start(SubsidiaryStart+Pos)
        /// </summary>
        public Vect2 Start
        {
            get => SubsidiaryStart + Pos;
            set => SubsidiaryStart = value - Pos;
        }
        /// <summary>
        /// Realy drawing end(SubsidiaryEnd+Pos)
        /// </summary>
        public Vect2 End
        {
            get => SubsidiaryEnd + Pos;
            set => SubsidiaryEnd = value - Pos;
        }
     
        public int Width { get; set; } = 1;
        /// <summary>
        /// Width multiple by <see cref="Entity.FullScale"/>. It is use to draw element on screen
        /// </summary>
        public LineEntity()
        {
            PrivateScale = new Vect2(1, 1);
        }


        private float RoundScale => (FullScale.X + FullScale.Y) / 2.0f;
        public Line GetLine() => GetScalingLine().GetBaseLine(); 
        public ScalingLine GetScalingLine() => new ScalingLine(new Line(Start, End), RoundScale);
        
        public void SetLine(Line line)
        {
            Start = line.Start;
            End = line.End;
        }
        public override Collider GetCollision()
        {
            return new Collider(this, GetLine() );

        }

        protected internal override void IfDraw(FixedBatch fixedBatch)
        {
            base.IfDraw(fixedBatch);
            Line line = GetLine();
            fixedBatch.DrawLine(line.Start, line.End , RenderColor,(Width));
        }
    }
}
