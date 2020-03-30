using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class ParticleColorByLife : ParticleSetter
    {

        public IEvaluate<Color> Evaluate { get; set; }
        public ParticleColorByLife(IEvaluate<Color> evaluate)
        {
            Evaluate = evaluate;
        }
  
        public override void Init(ParticleSettings settings, ParticleElement element)
        {
            element.RenderColor = Evaluate.Evaluate(Percent.Zero);
        }

        public override void Update(ParticleSettings settings, ParticleElement element)
        {
             element.RenderColor= Evaluate.Evaluate(Percent.Full- element.LifeTime.TotalSeconds/settings.LifeTime.TotalSeconds);
           
        }
    }
}
