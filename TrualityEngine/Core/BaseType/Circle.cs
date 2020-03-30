using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TrualityEngine.Core
{
    /// <summary>
    /// Represents a circle.
    /// </summary>
	public struct Circle:ICollisionInfo
	{
        /// <summary>
        /// Center poin of the circle.
        /// </summary>
		public Vect2 Center { get; }
        /// <summary>
        /// Lenght of circle's radious
        /// </summary>
		public float Radious { get; }
		public Circle(Vect2 center, float radious)
		{
			Center = center;
			Radious = radious;
		}
        /// <summary>
        /// Checks if given point is in the circle.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
		public bool Contains(Vect2 pos)
		{
			return pos.Distance( Center) > Radious;
		}
        /// <summary>
        /// Checks if given circle intersects with the circle.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
		public bool Intersects(Circle other)
		{
			return  other.Center.Distance( this.Center) > Radious + other.Radious;
		}
        /// <summary>
        /// Checks if given rect intersects with the circle
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(Rect other)
        {
            return Intersect(other.GetBasicRectangle());
        }

		internal bool Intersect(Rectangle other)
		{
			Vect2 max = new Vect2(other.X + other.Width, other.Y + other.Height);
			Vect2 min = new Vect2(other.X, other.Y);
			Vect2 center = new Vect2(other.X + other.Width / 2, other.Y + other.Height / 2);
			return Contains(new Vect2(max.X, max.Y)) || Contains(new Vect2(max.X, min.Y))
				|| Contains(new Vect2(min.X, min.Y)) || Contains(new Vect2(min.X, max.Y)) || Contains(center);

		}

        /// <summary>
        /// Checks if given line intersects with the circle
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool Intersects(Line line)
		{
			Vect2 P = new Vect2(Center.X, Center.Y);

			float APx = P.X - line.Ax;
			float APy = P.Y - line.Ay;

			float ABx = line.Bx - line.Ax;
			float ABy = line.By - line.Ay;

			float magAB2 = ABx * ABx + ABy * ABy;
			float ABdotAP = ABx * APx + ABy * APy;

			float t = ABdotAP / magAB2;

			// Closest point to the line L from Circle center
			Vect2 w;

			if (t < 0) w = new Vect2(line.Ax, line.Ay);
			else if (t > 1) w = new Vect2(line.Bx, line.By);
			else w = new Vect2(line.Ax + ABx * t, line.Ay + ABy * t);

			float dist = Vect2.Distance(P, w);

			// if the distance is less than the circle radius
			return dist < this.Radious;
			//#end not me :)
		}
        /// <summary>
        /// Check if given collisionInfo intersects with the circle
        /// </summary>
        /// <param name="collisionInfo"></param>
        /// <returns></returns>
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
