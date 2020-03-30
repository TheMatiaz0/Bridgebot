using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class ParticleRandomVelocity : ParticleSetter
    {


        public Direction Min { get; set; } = new Direction(-1, -1);
        public Direction Max { get; set; } = new Direction(1, 1);
        public ParticleRandomVelocity()
        {

        }

        public ParticleRandomVelocity(Vect2 min, Vect2 max)
        {
            Min = min;
            Max = max;
        }

        public override void Init(ParticleSettings settings, ParticleElement element)
        {
            element.Direction = Randomer.Base.NextVector2(Min,Max).ToDirection();
        }

        public override void Update(ParticleSettings settings, ParticleElement element)
        {

        }
    }
}
