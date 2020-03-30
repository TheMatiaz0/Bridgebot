using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class ParticleSizeByLife : ParticleSetter
    {
      
        public IEvaluate<float> Evaluated { get; }
        public override void Init(ParticleSettings settings, ParticleElement element)
        {
            
        }
        public ParticleSizeByLife(IEvaluate<float>  evaluated)
        {
            Evaluated = evaluated;
        }
        public ParticleSizeByLife(float start, float end):this(new Range(start, end)) { }
      
        public override void Update(ParticleSettings settings, ParticleElement element)
        {

            float scale = Evaluated.Evaluate( Percent.Full- new Percent( element.LifeTime.TotalSeconds , settings.LifeTime.TotalSeconds));
            element.PrivateScale = new Vect2(scale, scale) * settings.Size; 
        }
    }
}
