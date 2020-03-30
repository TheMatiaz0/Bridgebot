using Microsoft.Xna.Framework;
namespace TrualityEngine.Core
{
    public struct ScalingLine
    {

        private Line BaseLine { get; set; }

        public float Scale { get; set; }
        public ScalingLine(Line baseLine, float scale)
        {
            BaseLine = baseLine;
            Scale = scale;
        }
        public Line GetBaseLine() => new Line(BaseLine.Start - new Vect2(((Scale - 1) * BaseLine.Start.X) / 2, ((Scale - 1) * BaseLine.Start.Y) / 2), BaseLine.End + new Vect2(((Scale - 1) * BaseLine.End.X) / 2, ((Scale - 1) * BaseLine.End.Y) / 2));
    }
}
