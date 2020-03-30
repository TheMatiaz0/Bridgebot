using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public struct GradientPoint:IComparable<GradientPoint>,IEquatable<GradientPoint>
    {
      

        public Percent Pos { get; set; }
        public Color Color { get; set; }
        public GradientPoint(Percent pos, Color color)
        {
            Pos = pos;
            Color = color;
        }
        public int CompareTo(GradientPoint other)
        {
            return Pos.AsFloat.CompareTo(other.Pos.AsFloat);
        }
        public static bool operator ==(GradientPoint a,GradientPoint b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(GradientPoint a,GradientPoint b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            if (obj is GradientPoint gr)
                return gr.Color == this.Color && gr.Pos == this.Pos;
            else return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1290800521;
            hashCode = hashCode * -1521134295 + Pos.GetHashCode();
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            return hashCode;
        }

        public bool Equals(GradientPoint other)
        {
            return this.Equals(other);
        }
    } 
    public class Gradient:IEvaluate<Color>,ICloneable
    {
        public SortedSet<GradientPoint> Points { get; private set; }
      
        public Gradient(IEnumerable<GradientPoint> points)
        {
            Init(points);
        }
        private void Init(IEnumerable<GradientPoint> points)//I have some problem to classic invoking other constructor 
        {
            if (points == null)
                points = Enumerable.Empty<GradientPoint>();
            Points = new SortedSet<GradientPoint>(points);
        }
        public Gradient(params GradientPoint[] points) : this((IEnumerable<GradientPoint>)points) { }
        public Gradient(IEnumerable<Color> colors)
        {
             int count = colors.Count();
            IEnumerable<GradientPoint> points
                = colors?.Select(
                    (color, index)
                    => new GradientPoint((float)index / (count - 1), color));
            Init(points);
        }
        public Gradient(params Color[] colors) : this((IEnumerable<Color>)colors) { }
      
        public Gradient Clone()
        {
            return new Gradient((IEnumerable<GradientPoint>)Points);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
        /// <summary>
        /// Evaluate value. If Points.Count is equal 0, method will return a white color.
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public Color Evaluate(Percent percent)
        {
            if (Points.Count == 0)
                return Color.White;
            if (Points.Count == 1)
                return Points.First().Color;
            GradientPoint before=Points.First();//sorted set haven't a native index operator
           foreach(GradientPoint point in Points)
            {
                if (percent < point.Pos)
                {
                    Percent difference =percent - before.Pos;
                    Percent max = point.Pos - before.Pos;
                    Percent actualPercent = new Percent(difference, max);
                    return Color.Lerp(before.Color, point.Color, actualPercent);

                }
                else
                    before = point;
            }
            throw new Exception("Unexpected error during Evaluating");
        }

        
    }
}
