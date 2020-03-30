using System;

using static TrualityEngine.Core.SimpleDirection;
namespace TrualityEngine.Core
{
    [Serializable]
    public enum SimpleDirection
    {
        Empty = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        LeftUp = Left + Up,
        LeftDown = Left + Down,
        RightUp = Right + Up,
        RightDown = Right + Down,

    }
    public static class SimpleDirectionExtension
    {
        /// <summary>
        /// Convert to <see cref="Direction"/>.
        /// </summary>
        /// <param name="simpleDirection"></param>
        /// <returns></returns>
        public static Direction ToDirection(this SimpleDirection simpleDirection)
        {
            return simpleDirection switch
            {
                Empty => Direction.Zero,
                Up => Direction.Up,
                Down => Direction.Down,
                Left => Direction.Left,
                Right => Direction.Right,
                LeftUp => Direction.LeftUp,
                LeftDown => Direction.LeftDown,
                RightUp => Direction.RightUp,
                RightDown => Direction.RightDown,
                _ => throw new ArgumentException("uknown argument", nameof(simpleDirection)),
            };
            ;
        }
        /// <summary>
        /// It returns true if argument is <see cref="SimpleDirection.Down"/> or <see cref="SimpleDirection.Up"/>.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsConnectedWithY(this SimpleDirection dir)
        {
            switch (dir)
            {
                case Up: case Down: return true;
                default: return false;
            }
        }
        /// <summary>
        /// It returns true if argument is <see cref="SimpleDirection.Left"/> or <see cref="SimpleDirection.Right"/>
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsConnectedWithX(this SimpleDirection dir)
        {
            return !IsConnectedWithY(dir);
        }
    }
}
