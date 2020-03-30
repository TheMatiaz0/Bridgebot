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
    public struct Vect2 : IEquatable<Vect2>, IComparable<Vect2>, IComparable
    {
        public static readonly Vect2 Empty = new Vect2();
        public static readonly Vect2 One = new Vect2(1, 1);
        public static readonly Vect2 MinusOne = new Vect2(-1, -1);
        public static readonly Vect2
                Zero = new Vect2(0, 0),
                Up = new Vect2(0, 1),
                Down = new Vect2(0, -1),
                Left = new Vect2(-1, 0),
                Right = new Vect2(1, 0);

        private const string ParseError = "Error during trying to parse";

        public float X;
        public float Y;
        public Vect2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vect2(float val)
        {
            X = Y = val;
        }
        public Vect2(float[] vals)
        {
            X = vals[0];
            Y = vals[1];
        }
        public Vect2(Pair<float> pair)
        {
            X = pair.First;
            Y = pair.Second;
        }
        public Vect2(Direction direction)
        {
            X = direction.X;
            Y = direction.Y;
        }
        public Vect2(Resolution resolution)
        {
            X = resolution.Width;
            Y = resolution.Height;
        }
        internal Vector2 ToBasicVector2() => new Vector2(X, Y);
        internal static Vect2 FromBasic(Vector2 orginal)
          => new Vect2(orginal.X, orginal.Y);
        /// <summary>
        /// Rotating vector.
        /// It doesn't change itself. It will give you new <see cref="Vect2"/>.
        /// </summary>
        /// <returns></returns>
        public Vect2 Rotate(float angle)
        {
            return FromBasic(Vector2.Transform(ToBasicVector2(), Matrix.CreateRotationZ(new Rotation(angle).Radians)));
        }
        public float[] ToXYArray() => new float[] { X, Y };
        public Pair<float> ToPair() => new Pair<float>(X, Y);
        public Direction ToDirection() => new Direction(this);
        public Resolution ToResolution() => new Resolution(this);
        public Point ToPoint() => new Point((int)X, (int)Y);
        /// <summary>
        /// Geting abs value of vect. For example (-1,2) will be (1,2) after.
        /// It doesn't change itself. It will give you new <see cref="Vect2"/>.
        /// </summary>
        /// <returns></returns>
        public Vect2 Abs() => TheMath.Abs(this);
        public float GetAngleBeetwen(Vect2 other)
        {
            Vect2 a, b;
            a = FixedBatch.ToPrimitivePosition(this);
            b = FixedBatch.ToPrimitivePosition(other);
            return TheMath.ToDegreesFromRadians((float)Math.Atan2( b.Y - a.Y, b.X - a.X));

        }

        public float Lenght => TheMath.Sqrt(TheMath.Pow(X, 2) + TheMath.Pow(X, 2));
        public Vect2 Normalized => FromBasic(ToBasicVector2().GetNormalized());
        public Vect2 Revert()
            => new Vect2(-X, -Y);
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }
        public bool IsNormalize()
            => this.Normalized == this;
        public Vect2 Transform(Matrix matrix)
        {
            return Vect2.FromBasic(Vector2.Transform(this.ToBasicVector2(), matrix));
        }
        public float Distance(Vect2 b)
        {
            return Vector2.Distance(this.ToBasicVector2(), b.ToBasicVector2());
        }
        public Vect2 Reflect(Vect2 b)
        {
            return Vector2.Reflect(this, b);
        }
        public Vect2 Lerp(Vect2 other, float amount)
        {
            return Vector2.Lerp(this, other, amount);
        }
        public override string ToString()
        {
            return $"({X},{Y})";
        }
        public override int GetHashCode()
        {
            var hashCode = 57;
            hashCode = hashCode * 57 + X.GetHashCode();
            hashCode = hashCode * 57 + Y.GetHashCode();
            return hashCode;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vect2 v)
                return v == this;
            else
                return false;
        }

        public bool Equals(Vect2 other)
        {
            return this.Equals(other);
        }

        public int CompareTo(Vect2 other)
        {
            return this.Lenght.CompareTo(other.Lenght);
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is Vect2 v)
                return CompareTo(v);
            else
                return 0;
        }
        /// <summary>
        /// Parsing Vector from string.
        /// </summary>
        ///<exception cref="FormatException">Can have inner exception, but doesn't have to.</exception>
        /// <returns></returns>
        public static Vect2 Parse(string text)
        {

            string[] splited = text.Replace("(", String.Empty).Replace(")", String.Empty).Split(',');
            if (splited.Length != 2)
                throw new FormatException(ParseError);
            try
            {
                return new Vect2(float.Parse(splited[0]), float.Parse(splited[1]));
            }
            catch (Exception inner)
            {
                throw new FormatException(ParseError, inner);
            }

        }
        public static float Distance(Vect2 a, Vect2 b)
            => a.Distance(b);
        public static Vect2 operator +(Vect2 a, Vect2 b)
            => new Vect2(a.X + b.X, a.Y + b.Y);
        public static Vect2 operator -(Vect2 a, Vect2 b)
            => new Vect2(a.X - b.X, a.Y - b.Y);
        public static Vect2 operator -(Vect2 a)
            => a.Revert();
        public static Vect2 operator *(Vect2 a, Vect2 b)
            => new Vect2(a.X * b.X, a.Y * b.Y);
        public static Vect2 operator *(Vect2 a, float scale)
            => new Vect2(a.X * scale, a.Y * scale);
        public static Vect2 operator *(float scale, Vect2 b)
          => b * scale;
        public static Vect2 operator /(Vect2 a, Vect2 b)
            => new Vect2(a.X / b.X, a.Y / b.Y);
        public static Vect2 operator /(Vect2 a, float val)
            => new Vect2(a.X / val, a.Y / val);
        public static bool operator ==(Vect2 a, Vect2 b)
            => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vect2 a, Vect2 b)
            => !(a == b);
        public static implicit operator Vect2(Vector2 v)
            => FromBasic(v);
        public static implicit operator Vector2(Vect2 v)
            => v.ToBasicVector2();





    }

}
