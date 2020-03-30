 
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    /// <summary>
    /// ActivatedObject which know when is its Updating or its Starting or its Kill
    /// </summary>
    public abstract class Behaviour:ActivatedObject
    {
      
    
        protected List<SoundEffectInstance> SoundsInstances { get; private set; }=new List<SoundEffectInstance>();
        public SoundEffectInstance PlayUntilDeath(SoundEffect soundEffect)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            SoundsInstances.Add(soundEffectInstance);
            soundEffectInstance.Play();
            return soundEffectInstance;
        }
        public SoundEffectInstance PlayUntilDeath(string soundEffect)
        {
           return  PlayUntilDeath(Asset<SoundEffect>.Get(soundEffect).Value);
        }
        public override bool IsKill { get; protected  set; }
        public virtual bool CanUpdate => IsKill == false && IsActive;

        public Event<BaseBehaviourArgs> OnUpdate { get; protected set; } = Event.Empty;
        public Event<BaseBehaviourArgs> OnFixedUpdate { get; protected set; } = Event.Empty;
        /// <summary>
        /// It is called, when it is first update frame
        /// </summary>
        public Event<BaseBehaviourArgs> OnStart { get; protected set; } = Event.Empty;
        public Event<BaseBehaviourArgs> OnBeforeKilling { get; protected set; } = Event.Empty;


        private TimeSpan start;
        protected bool IsInit { get; private set; }

        internal static void Call<T>(IEnumerable<T> behaviours,Action<T> action)
            where T:Behaviour
        {
            
            foreach (T behaviour in behaviours.ToArray())
                if (behaviour.CanUpdate)
                    action(behaviour);
        }
       
       
        public override void Kill()
        {
            if (IsKill)
                throw new ObjectIsKilledException(this);

            IsKill = true;
            IfKill();
            foreach (var item in SoundsInstances)
                item.Stop();


        }
        protected virtual void IfKill()
        {
            OnBeforeKilling.Invoke(this, new BaseBehaviourArgs(this));
        }

        protected bool IsFirstFrame { get; private set; } = true;
        /// <summary>
        /// It is called, when it is first update frame
        /// </summary>
        protected virtual void IfStart()
        {
          
            IfActiveChange(true);
            IsInit = true;
            OnStart.Invoke(this, new BaseBehaviourArgs(this));
            start = TimeOfGame.Actual.TotalTime;
        }
       
     
        protected virtual void IfUpdate()
        {

            if (IsFirstFrame)
            {
                IsFirstFrame = false;
                IfStart();
            }

            OnUpdate.Invoke(this, new BaseBehaviourArgs(this));
        }
       
       

        public virtual void Update()
        {
            Yield.Update();
            while ((TimeOfGame.Actual.TotalTime - start) > TimeOfGame.Actual.FixedUpdateDelay&&this.IsKill==false)
            {
                start += TimeOfGame.Actual.FixedUpdateDelay;
                IfFixedUpdate();

            }
        }
        protected virtual void IfFixedUpdate()
        {
            OnFixedUpdate.Invoke(this, new BaseBehaviourArgs(this));
        }

    }
}
