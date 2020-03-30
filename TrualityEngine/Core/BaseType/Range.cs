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
    public struct Range : IEvaluate<float>
    {

        public static readonly Range ZeroOne = new Range(0, 1);
        public float Min { get; set; }
        public float Max { get; set; }
        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }
        public float Evaluate(Percent percent)
        {
            return Min + (Max - Min) * percent.AsFloat;
        }
    }

}
