using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    public class MethodDelay : ICloneable
    {

        public bool TimeScaled { get; set; } = true;

        public Coroutine Coroutine { get; private set; }
        public Action<MonoBehaviour> OnEnd { get; }
        public MonoBehaviour Mainer { get; }
        public int RepatingAmount { get; private set; }
        public bool IsBroken => Mainer == null || _Break;
        private bool _Break;
        public bool IsRuning => HasStarted && IsEnded == false;
        public bool HasStarted { get; private set; } = false;
        public bool IsEnded => _IsEnded || Mainer == null;
        private bool _IsEnded;
        public TimeSpan Delay { get; private set; }


        private readonly List<Action<MonoBehaviour>> onEndActions = new List<Action<MonoBehaviour>>();
        private readonly List<MethodDelay> onEndDelay = new List<MethodDelay>();

        public MethodDelay SetScaled(bool value)
        {
            TimeScaled = value;
            return this; 
        }


        public MethodDelay(MonoBehaviour mainer, TimeSpan delay, Action<MonoBehaviour> onEnd) : this(1, mainer, delay, onEnd)
        { }

        public MethodDelay(int repatingValue, MonoBehaviour mainer, TimeSpan delay, Action<MonoBehaviour> onEnd = null)
        {
            Mainer = mainer ?? throw new ArgumentNullException(nameof(mainer));

            OnEnd = onEnd;
            RepatingAmount = repatingValue;
            Delay = delay;

        }
        public MethodDelay(int repeatingAmount, MonoBehaviour mainer, float delay, Action<MonoBehaviour> onEnd = null)
            : this(repeatingAmount, mainer, TimeSpan.FromSeconds(delay), onEnd) { }


        /// <summary>
        /// End is always one. If <see cref="MethodDelay"/> is repeating, it'll be invoke after all.
        /// </summary>
        /// <param name="action"></param>
        public MethodDelay SetOnEnd(Action<MonoBehaviour> action, TimeSpan delay = default, int repeatingTime = 0)
        {
            MethodDelay methodDelay = new MethodDelay(repeatingTime, Mainer, delay, action);
            methodDelay.SetScaled(TimeScaled);
            onEndDelay.Add(methodDelay);
            return methodDelay;

        }
        /// <summary>
        /// End is always one. If <see cref="MethodDelay"/> is repeating, it'll be invoke after all.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public MethodDelay SetOnEnd(Action<MonoBehaviour> action, float time = 0, int repeatingTime = 1)
        {
            return SetOnEnd(action, TimeSpan.FromSeconds(time), repeatingTime);
        }

        /// <summary>
        /// Stoping if is running. If you want lock runing forever, use <see cref="BreakForever"></see>/>
        /// </summary>
        public void Stop()
        {
            if (Mainer != null && Coroutine != null)
                Mainer.StopCoroutine(Coroutine);
        }
        /// <summary>
        /// After use this, it cannot be runned.
        /// </summary>
        public void BreakForever()
        {
            _Break = true;
        }
        /// <summary>
        /// Run, it can be invoke only one time.
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            if (HasStarted || IsBroken)
                return false;
            HasStarted = true;
            if (Delay > TimeSpan.Zero)
                Coroutine = Mainer.StartCoroutine(WaitAndInvoke());
            else
                WhenEnd();

            return true;
        }
        private IEnumerator WaitAndInvoke()
        {

            yield return Async.NextFrame;
            Func<int, bool> conditions = (RepatingAmount >= 0)
                ? ((index) => index > 0)
                : (Func<int, bool>)(delegate { return true; });
            int loopToDo = RepatingAmount;
            while (Mainer != null && conditions(loopToDo--))
            {
                yield return Async.Wait(Delay,!TimeScaled);
                OnEnd.Invoke(Mainer);

            }
            WhenEnd();


        }
        private void WhenEnd()
        {
            if (_IsEnded)
            {
                throw new InvalidOperationException("This object is already ended");
            }
            if (Mainer != null)
            {
                foreach (var item in onEndActions)
                    item.Invoke(Mainer);
                foreach (var item in onEndDelay)
                    item.Run();
            }
            _IsEnded = true;
        }
        /// <summary>
        /// Cloning is legal only when object hasn't started yet.
        /// </summary>
        /// <returns></returns>
        public MethodDelay Clone()
        {
            if (HasStarted)
                throw new InvalidOperationException("It's impossible to clone object which has started");
            MethodDelay delay = (MethodDelay)base.MemberwiseClone();
            return delay;

        }
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}

