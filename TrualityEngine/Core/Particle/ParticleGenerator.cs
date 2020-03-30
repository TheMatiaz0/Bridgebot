using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TrualityEngine.Core
{
  
   
    public class ParticleGenerator:Entity
    {
        
        public List<ParticleSetter> Setters { get; private set; } = new List<ParticleSetter>();
        public IReadOnlyList<ParticleSetter> GetSetters() => Setters;  
        public ParticleSettings ParticleSettings { get; private set; } = new ParticleSettings(); 
        public TimeSpan DelayFromNext { get; set; } = TimeSpan.FromSeconds(0.2f);
        public uint InOneShoot { get; set; } = 1;
        public uint MaxShoot { get; set; } = 0;
        public bool IsPlaying { get; private set; } = false;
        public uint CurrentShoot { get; private set; } = 0;
        private bool playOnStart;
        public Event<BaseEntityArgs> OnStop { get; protected set; } = Event.Empty;
        
       
        public static ParticleGenerator FastSingle(ParticleSettings settings,
            TimeSpan delayFromNext, uint inOneShoot,uint MaxShoot
            ,Vect2 pos,params ParticleSetter[] setters)
        {
            var generator= new ParticleGenerator(settings, true)
            {
                DelayFromNext = delayFromNext,
                InOneShoot = inOneShoot,
                MaxShoot = MaxShoot,
                Pos = pos,
            };
            foreach (ParticleSetter setter in setters)
                generator.AddSetter(setter);
            generator.OnStop.Value += (s, e) => e.AsEntity.Kill();
            return generator;

        }
        public ParticleGenerator()
        {

        }
        public ParticleGenerator(bool playOnStart)
        {
            this.playOnStart = playOnStart;
        }
        public ParticleGenerator(ParticleSettings settings,bool playOnStart=false)
        {
            ParticleSettings = settings;
            this.playOnStart = playOnStart; ;
        }
        protected override void IfStart()
        {
            base.IfStart();
            Yield.Start(Spawner());
            if (playOnStart)
                PlayParticle();
        }
        public void PlayParticle()
        {
            IsPlaying = true;
            CurrentShoot = 0;

        }
        public void StopParticle()
        {
            IsPlaying = false;
            CurrentShoot = 0;
        }
        public ParticleSetter AddSetter(ParticleSetter particle)
        {
            if (Setters.Any(item => item.GetType() == particle.GetType()) == false)
            {
                Setters.Add(particle);
                return particle;
            }
            return null;
           
               
        }
        public T AddSetter<T>()
            where T: ParticleSetter,new()
        {
            return (T)AddSetter( new T());
        }
        public T GetSetter<T>()
            where T:ParticleSetter
        {
            return (T)Setters.Find(item => item.GetType() == typeof(T));
        }
        public bool RemoveSetter(ParticleSetter particle)
        {
            return Setters.Remove(particle);
        }
        
        private IEnumerator<ICoroutineable> Spawner()
        {
            while(true)
            {
                yield return Async.WaitTime(DelayFromNext);
                if (MaxShoot!=0&& CurrentShoot >= MaxShoot)
                {
                    StopParticle();
                }
                  
                if(IsPlaying==false)
                {
                    yield return Async.WaitTo(t => IsPlaying == true);
                    continue;
                }

                CurrentShoot++;
                for(int x=0;x<InOneShoot;x++)
                {
                    ParticleElement element = new ParticleElement(this)
                    { Sprite = ParticleSettings.Sprite };
                    element.ColliderManager.IsActive = false;
                    element.Pos = this.Pos;
                }
              




            }
        }

        public override void Update()
        {
            base.Update();
            
        }
    }
}

