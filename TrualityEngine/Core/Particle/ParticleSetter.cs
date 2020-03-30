using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrualityEngine.Core
{
   
    public abstract class ParticleSetter:ICloneable
    {
        public ParticleSetter Clone()
        {
            return (ParticleSetter)MemberwiseClone();
        }


        public abstract void Init(ParticleSettings settings, ParticleElement element);
        public abstract void Update(ParticleSettings settings, ParticleElement element);

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
