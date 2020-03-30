using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public struct Rotation
    {
        private const float MaxAngle = 360;
        public readonly static Rotation Zero = new Rotation();
        public readonly static Rotation Half = new Rotation(MaxAngle/2);
        private float _Angle;
        public float Angle
        {
            get => _Angle;
            set
            {
                if (value == _Angle)
                    return;
               
                _Angle = value%MaxAngle;

            }
        
        }

        public Rotation(float angle)
        {
            _Angle = 0;
            Angle = angle;
        }
        



        public static Rotation operator +(Rotation a,Rotation b)
        {
            return new Rotation(a.Angle + b.Angle);
        }
        public static Rotation operator -(Rotation a, Rotation b)
        {
            return new Rotation(a.Angle - b.Angle);
        }
        public static Rotation operator *(Rotation a,Rotation b)
        {
            return new Rotation(a.Angle * b.Angle);
        }
        public static Rotation operator/(Rotation a, Rotation b)
        {
            return new Rotation(a.Angle / b.Angle);
        }
        public static Rotation operator *(Rotation a, float b)
        {
            return new Rotation( a.Angle * b);
        }
        public static Rotation operator/(Rotation a, float b)
        {
            return new Rotation(a.Angle / b);
        }
        

        public static Rotation FromRadians(float radians)
        {
            return new Rotation() { Radians = radians };
        }
        public static Rotation FromDegrees(float degrees)
        {
            return new Rotation(degrees);
        }
        public float Radians
        {
            get => MathHelper.ToRadians(Angle);
            set
            {
                Angle= MathHelper.ToDegrees(value);
            }
        }

        public override string ToString()
        {
            return $"{Angle}°";
        }
    }
}
