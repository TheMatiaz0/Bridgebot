using Microsoft.Xna.Framework;
using System;

namespace TrualityEngine.Core
{
    /// <summary>
    /// Represents line in 2d space
    /// </summary>

    public struct Line:ICollisionInfo
    {


        public readonly static Line Empty = new Line();


        public Vect2 Start { get; private set; }

        public Vect2 End { get; private set; }
        public float Ax => Start.X;
        public float Bx => End.X;
        public float Ay => Start.Y;
        public float By => End.Y;
        public Line(Vect2 start, Vect2 end)
        {

            Start = start;
            End = end;
        }
        public Line(float x1, float y1, float x2, float y2) : this(new Vect2(x1, y1), new Vect2(x2, y2))
        {

        }
        public Vect2 GetCenter()
        {
            return End - (End - Start) / 2f;
        }
        public bool Contains(Vect2 pos)
        {
            return Intersect(new Rectangle(pos.ToPoint(), new Point(1, 1)));
        }
        public bool Intersects(Circle other)
        {
            return other.Intersects(this);
        }
        public bool Intersects(Line other)
        {
            //#not me
            float denominator = ((End.X - Start.X) * (other.End.Y - other.Start.Y)) - ((other.End.Y - Start.Y) * (other.End.X - other.Start.X));
            float numerator1 = ((Start.Y - other.Start.Y) * (other.End.X - other.Start.X)) - ((Start.X - other.Start.X) * (other.End.Y - other.Start.Y));
            float numerator2 = ((Start.Y - other.Start.Y) * (End.X - Start.X)) - ((Start.X - other.Start.X) * (End.Y - Start.Y));

            // Detect coincident lines (has a problem, read below)
            if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
            //#end not me
        }

        public bool Intersects(Rect other)
            => LineIntersectsRect(Start, End, other.GetBasicRectangle());
        internal bool Intersect(Rectangle other)
            => LineIntersectsRect(Start, End, other);
        private static bool LineIntersectsRect(Vect2 p1, Vect2 p2, Rectangle r)
        {
            //#not me
            static bool LineIntersectsLine(Vect2 l1p1, Vect2 l1p2, Vect2 l2p1, Vect2 l2p2)
            {
                float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
                float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

                if (d == 0)
                {
                    return false;
                }

                float rr = q / d;

                q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
                float s = q / d;

                if (rr < 0 || rr > 1 || s < 0 || s > 1)
                {
                    return false;
                }

                return true;
            }
            return LineIntersectsLine(p1, p2, new Vect2(r.X, r.Y), new Vect2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vect2(r.X + r.Width, r.Y), new Vect2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vect2(r.X + r.Width, r.Y + r.Height), new Vect2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vect2(r.X, r.Y + r.Height), new Vect2(r.X, r.Y)) ||
                   (r.Contains(p1.ToBasicVector2()) && r.Contains(p2.ToBasicVector2()));
            //#end not me
        }

        public static bool operator ==(Line a, Line b)
             => a.Start == b.Start && a.End == b.End;
        public static bool operator !=(Line a, Line b)
            => !(a == b);
        public override bool Equals(object obj)
        {
            if (obj is null || (obj is Line == false))
                return false;
            else return ((Line)obj) == this;
        }
        public override string ToString()
        {
            return $"Line:" +
                $"Start:{Start},End:{End}";

        }
        public override int GetHashCode()
        {
            int code = -11;
            code = code * 13 + Start.GetHashCode();
            code = code * 13 + End.GetHashCode();
            return code;
        }
      

        public bool Intersects(ICollisionInfo collisionInfo)
          => collisionInfo switch
          {
              Rect r => this.Intersects(r),
              Circle c => this.Intersects(c),
              Line l => this.Intersects(l),
              null => false,
              _ => throw new ArgumentException("Uknown type", nameof(collisionInfo)),
          };


    }
}
