using Microsoft.Xna.Framework;
using System;
namespace TrualityEngine.Core
{

    /// <summary>
    /// It's kind of <see cref="Vect2"/>, but  it is always normalized jest k?
    /// </summary>
    public struct Direction:IEquatable<Direction>
    {
      
        private const string BadValueException = "Argument cannot be different that -1 to 1, if you want to set normalized value, you should use constructor";
        private const string OutOfRangeException = "This arrat size is as 0-1";

        /// <summary>
        /// X direction value..
        /// </summary>
        public float X
        {
            get => _X;
            set
            {
                if (Math.Abs(value) <= 1.0)
                    _X = value;
                else throw new ArgumentException(BadValueException);


            }
        }
        /// <summary>
        /// Y direction value.
        /// </summary>
        public float Y
        {
            get => _Y; 
            set
            {
                if (Math.Abs(value) <= 1.0)
                    _Y = value;
                else throw new ArgumentException(BadValueException);
            }
        }
        /// <summary>
        /// Gives elements by the index.0 gives you the X, 1 gives you the Y.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public  float this[int index]
        {
            get => index switch
            {
                0 => X,
                1 => Y,
                _=> throw new IndexOutOfRangeException(OutOfRangeException)
            };
            set
            {
                switch (index)
                {
                    case 0: X = value;
                        break;
                    case 1:Y = value;
                        break;
                    default:throw new IndexOutOfRangeException(OutOfRangeException);
                }

            }


           
        }

        public static readonly Direction
          Zero = new Direction(0,0),
          Up = new Direction(0, 1),
          Down = new Direction(0, -1),
          Left = new Direction(-1, 0),
          Right = new Direction(1, 0),
          LeftUp = Up + Left,
          LeftDown = Down + Left,
          RightUp = Up + Right,
          RightDown = Down + Right;
        private float _X;
        private float _Y;

        public Direction(float x, float y)
        {
            var vect = new Vect2(x, y);
            vect = vect.Normalized;
            _X = vect.X;
            _Y = vect.Y;
        }
        public Direction(Vect2 v) : this(v.X, v.Y)
        {

        }
        /// <summary>
        /// Converts to a <see cref="Vect2"/>. It never affect to values.
        /// </summary>
        /// <returns></returns>
        public Vect2 ToVect2()
            => new Vect2(X, Y);
        /// <summary>
        /// Gives a array with the X and the Y.
        /// </summary>
        /// <returns></returns>
        public float[] ToFloatArray() => new float[] { X, Y };
        public SimpleDirection? TryToSimpleDirection()
        {


            if (ToVect2()== Vect2.Zero)
                return SimpleDirection.Empty;

            SimpleDirection? xOptions = null;
            SimpleDirection? yOptions = null;

            if (X > 0)
                xOptions = SimpleDirection.Right;
            else if (X < 0)
                xOptions = SimpleDirection.Left;
            if (Y > 0)
                yOptions = SimpleDirection.Up;
            else if (Y < 0)
                yOptions = SimpleDirection.Down;


            if (xOptions == null && yOptions == null)
                return null;

            if (yOptions == null)
                return xOptions;
            else if (xOptions == null)
                return yOptions;
            else
                return ((int)xOptions + yOptions);

        }
        /// <summary>
        /// Method can throw exception
        /// </summary>
        /// <param name="minimalRound"></param>
        /// <returns></returns>
        public SimpleDirection ToSimpleDirection()
        {
            return TryToSimpleDirection() ?? throw new Exception("Failed convert to SimpleDirection");
        }


        /// <summary>
        /// Gives new <see cref="Direction"/> rotated by the angle
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public Direction Rotate(float angle)
        {
            return ToVect2().Rotate(angle).ToDirection();
        }
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }
        public override bool Equals(object obj)
        {
            if (obj is Direction dir)
                return this == dir;
            else
                return false;
        }

        private static Direction Operator(Direction a, Direction b, Func<Vect2, Vect2, Vect2> func)
            => func(a, b).Normalized;

        public override int GetHashCode()
        {
            return (int)(X + Y);
        }
        public override string ToString()
            => $"X:{X},Y:{Y}";

        bool IEquatable<Direction>.Equals(Direction other)
        {
            return this == other;
        }

     
        public static Vect2 operator *(Direction a, Vect2 b)
        {
            return a.ToVect2() * b;
        }
        public static Vect2 operator*(Direction a, float b)
        {
            return a.ToVect2() * b;
        }
        public static Direction operator +(Direction a, Direction b)
            => Operator(a, b, (x, y) => x + y);
        public static Direction operator -(Direction a, Direction b)
            => Operator(a, b, (x, y) => x - y);
        public static Direction operator /(Direction a, Direction b)
            => Operator(a, b, (x, y) => x / y);
        public static Direction operator *(Direction a, Direction b)
           => Operator(a, b, (x, y) => x * y);
        public static Direction operator -(Direction a)
            => -a.ToVect2();
        public static bool operator ==(Direction a, Direction b)
            => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Direction a, Direction b)
            => !(a == b);

        public static implicit operator Vect2(Direction dir)
            => dir.ToVect2();

        public static implicit operator Direction(Vect2 vect)
            => vect.ToDirection();
        public static implicit operator Direction(SimpleDirection dir)
            =>dir.ToDirection();
        public static explicit operator SimpleDirection(Direction dic)
            =>dic.ToSimpleDirection();


    }
}
