using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public class ParticleElement:SpriteEntity
    {
        public ParticleGenerator Main { get; }
        private ParticleSetter[] Logics { get; }
        public TimeSpan LifeTime { get; private set; }
        private ParticleSettings Settings { get; set; }
        public Direction Direction { get; set; }

        public ParticleElement(ParticleGenerator particleEntity)
        {
            if (particleEntity == null)
                throw new ArgumentNullException(nameof(particleEntity));
            else if (particleEntity.IsKill == true)
                throw new ObjectIsKilledException(particleEntity);

            Main = particleEntity;
            Logics = new ParticleSetter[particleEntity.GetSetters().Count];
            int index = 0;
            Settings = particleEntity.ParticleSettings.Clone();
            LifeTime = particleEntity.ParticleSettings.LifeTime;
            RenderColor = particleEntity.ParticleSettings.Color;
            PrivateScale = particleEntity.ParticleSettings.Size;
            foreach (ParticleSetter particleLogic in particleEntity.GetSetters() )
            {
                ParticleSetter current;
                Logics[index++] =current= particleLogic.Clone();
                current.Init(Settings, this);

            }
          
           
            
        }
        protected override void IfUpdate()
        {
            base.IfUpdate();

          foreach(ParticleSetter setter in Logics)
            {
                setter.Update(Settings, this);
            }
            LifeTime -= TimeOfGame.Actual.DeltaTime;
            if (LifeTime <= TimeSpan.Zero)
                this.Kill();
            this.Pos += Direction.ToVect2() * (float)TimeOfGame.Actual.DeltaTime.TotalSeconds * Settings.Speed;

            
        }


    }
}