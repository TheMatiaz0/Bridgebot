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
namespace TrualityEngine.Core
{
    public struct Rect :ICollisionInfo
    {
        private Rectangle baseRect;
       public int Width
        {
            get => baseRect.Width;
            set => baseRect.Width = value;
        }
        public int Height
        {
            get => baseRect.Height;
            set => baseRect.Height = value;
        }

        internal Rectangle GetBasicRectangle() => baseRect;
        public Point Pos
        {
            get => baseRect.Location;
            set => baseRect.Location = value;
        }
        public Point Size
        {
            get => baseRect.Size;
            set => baseRect.Size = value;
        }
        public int X
        {
            get => baseRect.X;
            set => baseRect.X = value;

        }
        public int Y
        {
            get => baseRect.Y;
            set => baseRect.Y = value;
        }
        public Rect(int x, int y, int width, int height)
        {
            baseRect = new Rectangle(x, y, width, height);
        }
        public Rect(int x, int y, Point size) : this(x, y, size.X, size.Y) { }
        public Rect(Point pos, Point size) : this(pos.X, pos.Y, size.X, size.Y) { }
        public bool Intersects(Line line)
        {
            return line.Intersects(this);
        }
        public bool Intersects(Circle circle)
        {
            return circle.Intersects(this);
        }
        public bool Intersects(Rect rect)
        {
            return baseRect.Intersects(rect.baseRect);
        }
        public IEnumerable<Line> GetLines()
        {
            yield return new Line(Pos.ToVector2(),new Vect2(Pos.X,Pos.Y+Height));
            yield return new Line(new Vect2(Pos.X, Pos.Y + Height), new Vect2(X + Width, Y + Height));
            yield return new Line(new Vect2(X + Width, Y + Height), new Vect2(X + Width, Y));
            yield return new Line(new Vect2(X + Width, Y), Pos.ToVector2());

        }
        public IEnumerable<Line> GetLeftRightLines()
        {
            yield return new Line(Pos.ToVector2(), new Vect2(Pos.X, Pos.Y + Height));
            yield return new Line(new Vect2(X + Width, Y + Height), new Vect2(X + Width, Y));
        }
        public IEnumerable<Line> GetUpDownLines()
        {
            yield return new Line(new Vect2(Pos.X, Pos.Y + Height), new Vect2(X + Width, Y + Height));
            yield return new Line(new Vect2(X + Width, Y), Pos.ToVector2());
        }

        public bool Intersects(ICollisionInfo collisionInfo) 
            => collisionInfo switch
        {
            Rect r => this.Intersects(r),
            Circle c => this.Intersects(c),
            Line l => this.Intersects(l),
            null => false,
            _ => collisionInfo.Intersects(this),
        };

        public bool Contains(Vect2 pos)
        {
            return baseRect.Contains(pos);
        }
    }

}
