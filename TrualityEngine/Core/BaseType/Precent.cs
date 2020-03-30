using System;
namespace TrualityEngine.Core
{

    public struct Percent
    {

        public const double MaxValue = 1.0;
        public const double MinValue = 0.0;
       
        public static readonly Percent Zero = new Percent();
        public static readonly Percent Half = new Percent(0.5f);
        public static readonly Percent Full = new Percent(1);
        private double _Value;
        public double AsDoubleValue
        {
            get => _Value;
           
        }
        private static double Clamp(double val)
        {
           return Math.Max(Math.Min(val, 1f), 0f);
        }
        public byte AsProcentValue => (byte)(_Value * 100);
        public float AsFloat => (float)AsDoubleValue;
        /// <summary>
        /// Give value that equal 0.0-1.0
        /// </summary>
        /// <param name="decimal"></param>
        public Percent(double @decimal)
        {

            _Value = Percent.Clamp(@decimal);

        }
        /// <summary>
        /// Creating procent equal value/full
        /// </summary>
        /// <param name="value"></param>
        /// <param name="full"></param>
        public Percent(double value, double full)
        {
            if (value > full)
                throw new ArgumentException("value cannot be greater that full", nameof(value));
            _Value = Percent.Clamp(value / full);
        }
        /// <summary>
        /// Give value that equal 0-100
        /// </summary>
        /// <param name="val"></param>
        public static Percent FromPercent(byte val)
        {
            return new Percent(val / 100.0);
        }
        /// <summary>
        ///  Give value that equal 0.0-1.0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Percent FromDecimal(double val)
        {
            return new Percent(val);
        }
        public double Different(Percent other)
           => this.AsDoubleValue - other.AsDoubleValue;
        public static bool operator ==(Percent a, Percent b)
            => a.AsDoubleValue == b.AsDoubleValue;
        public static bool operator !=(Percent a, Percent b)
            => !(a == b);
        public static bool operator >(Percent a, Percent b)
            => a.AsDoubleValue > b.AsDoubleValue;
        public static bool operator <(Percent a, Percent b)
            => a.AsDoubleValue < b.AsDoubleValue;
        public static bool operator <=(Percent a, Percent b)
            => a == b || a < b;
        public static bool operator >=(Percent a, Percent b)
          => a == b || a > b;
        public static implicit operator double(Percent procent)
            => procent.AsDoubleValue;
        public static implicit operator float(Percent procent)
            => procent.AsFloat;
        public static explicit operator Percent(float value)
            => new Percent(value);
        public static implicit operator Percent(double value)
            => new Percent(value);
        public static Percent operator +(Percent a, double b)
         => new Percent(Math.Min(1, a.AsDoubleValue + b));
        public static Percent operator -(Percent a, double b)
        => new Percent(Math.Max(0, a.AsDoubleValue - b));
        public static Percent operator *(Percent a, double b)
            => new Percent(Math.Min(1, a.AsDoubleValue * b));
        public static Percent operator /(Percent a, double b)
            => new Percent(Math.Min(1, a.AsDoubleValue / b));
        public static Percent operator +(Percent a, Percent b)
            => a + b.AsDoubleValue;
        public static Percent operator -(Percent a, Percent b)
            => a - b.AsDoubleValue;
        public static Percent operator *(Percent a, Percent b)
            => a * b.AsDoubleValue;
        public static Percent operator /(Percent a, Percent b)
            => a / b.AsDoubleValue;


        public override bool Equals(object obj)
        {
            if (obj is Percent p)
                return this == p;
            else return false;
        }
        public override string ToString()
        {
            return $"{AsDoubleValue * 100}%";
        }
        public override int GetHashCode()
        {
            return AsDoubleValue.GetHashCode();
        }
    }
}
