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
    /// <summary>
    /// Represents a bezier curve. Useful for smoth fades.
    /// </summary>
    public struct BezierCurve:IEvaluate<float>
    {
        public struct BezierPoint
        {
            public Percent X;
            public float Y;

            public BezierPoint(Percent x, float y)
            {
                X = x;
                Y = y;
            }
        }
        /// <summary>
        /// Start.
        /// </summary>
        public BezierPoint A;
        /// <summary>
        /// First help point.
        /// </summary>
        public BezierPoint B;
        /// <summary>
        /// Second help point.
        /// </summary>
        public BezierPoint C;
        /// <summary>
        /// End.
        /// </summary>
        public BezierPoint D;

        public BezierCurve(BezierPoint a, BezierPoint b, BezierPoint c, BezierPoint d)
        {
            A = a;
            B = b;
            C = c; 
            D = d;
        }
        public BezierCurve(Percent p2x, float p2y,Percent p3x, float p3y)
            :this(new BezierPoint(0,0), new BezierPoint(p2x,p2y), new BezierPoint(p3x,p3y), new BezierPoint(1,1))
        { }
      
        public BezierCurve( Percent p1x, float p1y, Percent p2x, float p2y, Percent p3x, float p3y, Percent p4x, float p4y)
            :this(new BezierPoint(p1x,p1y), new BezierPoint(p2x,p2y), new BezierPoint(p3x,p3y), new BezierPoint(p4x,p4y))
        { }

        /// <summary>
        /// Calculate "y" for conreate "x".
        /// P(x)=A(1-x)^3+3Bx(1-x)^2+3Cx^2(1-x)+Dx^3
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float Evaluate(Percent x)
        {       
            return A.Y * (TheMath.Pow(1 - x.AsFloat, 3))
                + 3 * B.Y * x.AsFloat * (TheMath.Pow(1 - x.AsFloat, 2))
                + 3 * C.Y * TheMath.Pow(x.AsFloat, 2) * (1 - x.AsFloat)
                + D.Y * TheMath.Pow(x.AsFloat, 3);
        }
        public static readonly BezierCurve Ease = new BezierCurve(.25f, .1f, .25f, 1f);
        public static readonly BezierCurve BigEase = new BezierCurve(.05f, .4f, 0, 1);
        public static readonly BezierCurve Linear = new BezierCurve(0, 0, 1, 1);
        public static readonly BezierCurve EaseIn = new BezierCurve(.42f, 0, 1, 1);
        public static readonly BezierCurve BigEaseIn = new BezierCurve(1, 0, 1, 1);
        public static readonly BezierCurve EaseInOut = new BezierCurve(.42f, 0, .58f, 1f);
        public static readonly BezierCurve PositveSin = new BezierCurve(.42f, 0, .58f, 1f);
        public static readonly BezierCurve S = new BezierCurve(1, 0, 0, 1);
        public static readonly BezierCurve TwoMountains = new BezierCurve(.03f, 2, 1f, -0.76f);
        public static readonly BezierCurve Hole = new BezierCurve(0, 1.83f, 1, -1.31f);
    }
}
