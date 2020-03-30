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
  
    public class Tween : IKillable,ICoroutineable
    {
        public delegate TValue TweenGetter<TValue>(float value, TValue startValue, TValue endValue);
        public delegate void TweenSetter<TValue>(float value, TValue start, TValue final);
        private sealed class TW< TValue> : Tween
        {
            private TweenSetter<TValue> action;
            private Func<TValue> startGetter, endGetter;
            private TValue start, end;
            private IEvaluate<float> evaluater;
            public TW(TweenSetter<TValue> setter, TimeSpan time, IEvaluate<float> evaluater
                ,Func<TValue> startGetter,Func<TValue> endGetter,IKillable killable) : base( time,killable)
            {
                this.action = setter ?? throw new ArgumentNullException(nameof(setter));
                this.startGetter = startGetter ?? throw new ArgumentNullException(nameof(startGetter));
                this.endGetter = endGetter ?? throw new ArgumentNullException(nameof(endGetter));
                this.evaluater = evaluater ?? Range.ZeroOne;
            }


            protected override void UpdateTween(TimeSpan time)
            {
               action(Math.Min(1, evaluater.Evaluate((float)(time.TotalSeconds / FullTime.TotalSeconds))), start, end);
            }
            protected override void OnTweenStart()
            {
                base.OnTweenStart();
                start = startGetter();
                end = endGetter();

            }
        }


        private static Entity Entity { get;  set; }
        private static readonly Dictionary<IKillable, List<Tween>> tweensDictionary = new Dictionary<IKillable, List<Tween>>();
        public bool IsKill { get; private set; }
        public bool IsActive => false;
        public TimeSpan FullTime { get; }
        public bool IsRunning => coroutine != null;
        public bool HasEnded { get; private set; }
        public int RepeatAmount { get; private set; }
        public TimeSpan RepeatDelay { get; private set; }
        public IKillable Target { get;   }

        private Coroutine coroutine;
        private Action onEnd;
        private Action onTrueEnd;
        internal Tween(TimeSpan time, IKillable killable)
        {
          
            FullTime = time;
            Target = killable ?? throw new ArgumentNullException(nameof(killable));
         
        }
        /// <summary>
        /// </summary>
        /// <param name="onEnd"></param>
        /// <returns>Returns itself.</returns>
        public Tween SetOnEnd(Action onEnd)
        {
            KillTest();
         
            this.onEnd = this?.onEnd + onEnd ?? onEnd;
            
            return this;
        }
        public Tween SetOnEndAfterAllRepeat(Action onEnd)
        {

            KillTest();
            onTrueEnd = this.onTrueEnd + onEnd ?? onEnd;
            return this;
        }
        private void KillTest()
        {
            if (IsKill)
                throw new ObjectIsKilledException(this);
        }
       
        private Tween RawStart()
        {
            Entity ??= new Entity()
            {
                IsGlobal = true,
                Name="_TweenModel"
                
            };
            coroutine = Entity.Yield.Start(this.Do());
            return this;
        }
        /// <summary>
        /// </summary>
        /// <returns>Returns itself</returns>
        public Tween Start()
        {
            if (IsRunning)
                return this;
            if (IsKill)
                throw new ObjectIsKilledException(this);
            return RawStart();
          
            
        }
        protected  IEnumerator<ICoroutineable> Do()
        {
            if(tweensDictionary.ContainsKey(Target))
            {
                tweensDictionary[Target].Add(this);
            }
            else
            {
                tweensDictionary[Target] = new List<Tween>() { this};
            }
            OnTweenStart();
            TimeSpan time = TimeSpan.Zero;
           
            while (time < FullTime)
            {

                yield return Async.WaitOneFrame();
                if (Target.IsKill)
                    this.Cancel();

                time += TimeOfGame.Actual.DeltaTime;
                UpdateTween(time);

            }
            onEnd?.Invoke();

            if (RepeatAmount != 0 && RepeatAmount !=1)
            {
                if (RepeatAmount > 0)
                    RepeatAmount--;
                if (RepeatDelay > TimeSpan.Zero)
                    EObject.StaticMachine.InvokeWithDelay
                        (() =>
                        {
                            RawStart();
                        }, RepeatDelay);
                else
                    RawStart();

            }
            else
            {
                coroutine = null;
                HasEnded = true;
            }
            tweensDictionary[Target].Remove(this);
            if (tweensDictionary[Target].Count == 0)
                tweensDictionary.Remove(Target);
        }
      
        public Tween SetRepeat(int repeatVal,TimeSpan? delay=null)
        {
            RepeatDelay = delay ?? TimeSpan.Zero;
            RepeatAmount = repeatVal;
            return this;
        }
        public void Cancel()
        {
            coroutine?.Cancel();
            IsKill = true;
        }
        public void Cancel(bool doOnSimpleComplete,bool doOnTrueComplete)
        {
            if (doOnSimpleComplete)
                onEnd?.Invoke();
            if (doOnTrueComplete)
                onTrueEnd?.Invoke();
            Cancel();
        }
        public void Cancel(bool doOnComplete)
        {
            Cancel(doOnComplete, doOnComplete);
        }
        bool ICoroutineable.GetIsDone()
        {
            return HasEnded;
        }
        protected virtual void UpdateTween(TimeSpan time) { }
        protected virtual void OnTweenStart() { }

        #region STATIC
        public static void CancelAll(IKillable target,bool doOnSimpleComplete,bool doOnTrueComplete)
        {
          if(  tweensDictionary.TryGetValue(target,out List<Tween> value))
            {
                foreach (var item in value)
                    item.Cancel(doOnSimpleComplete, doOnTrueComplete);
            }
         
        }
        public static void CancelAll(IKillable target,bool doOnComplete)
        {
            CancelAll(target, doOnComplete, doOnComplete);
        }
        public static void CancelAll(IKillable target)
        {
            CancelAll(target, false, false);
        }
        public static void CancelAll(bool doOnSimpleComplete,bool doOnTrueComplete)
        {
            foreach (var key in tweensDictionary)
                foreach (var item in key.Value)
                    item.Cancel(doOnSimpleComplete,doOnTrueComplete);
                   
        }
        public static void CancelAll(bool doOnComplete)
        {
            CancelAll(doOnComplete, doOnComplete);
        }
        public static void CancelAll()
        {
            CancelAll(false, false);
        }
        public static Tween MakeCustom<TItem,TValue>(TItem item,Func<TValue> startGetter, Func<TValue> finalGetter, TimeSpan time, IEvaluate<float> evaluater,
            Action<TItem,TValue> setter, TweenGetter<TValue> getter)
        {

            IKillable killable = item as IKillable;
            killable ??= EObject.StaticMachine;
        

            return new Tween.TW<TValue>(
                (value,start,final) =>
                {
                    setter(item, getter(value, start, final));
                },
                time,
                evaluater,
                startGetter,
                finalGetter, killable).Start();
        }


        //I cant use generic because in c# there is no any general interface for arthmetic operators , i can use dynamics but it's not fast enough
        //I know that it looks like unecessary repeat, but there is only way.

      
        public static Tween MakeCustom<TItem>(TItem item,  Func<Vect2> final, TimeSpan time,IEvaluate<float> evaluater, Func<Vect2> start, Action<TItem,Vect2> setter)
            => MakeCustom(item, start, final, time, evaluater,setter, (val, onStart, final) => (final - onStart) * val + onStart);
        public static Tween MakeCustom<TItem>(TItem item,  Func<float> final, TimeSpan time, IEvaluate<float> evaluater, Func<float> start ,Action<TItem, float> setter)
            => MakeCustom(item, start, final, time, evaluater, setter, (val, onStart, final) => (final - onStart) * val + onStart);

        public static Tween MakeCustom<TItem>(TItem item, Func<Color> final, TimeSpan time, IEvaluate<float> evaluater, Func<Color> start, Action<TItem, Color> setter)
         => MakeCustom(item, start, final, time, evaluater, setter, (val, onStart, final) => Color.Lerp(onStart, final, val));
        public class ForEntities
        {
            public static Tween Scale(Entity entity, Vect2 final, TimeSpan time, IEvaluate<float> evaluater = null)
                => MakeCustom(entity, () => final, time, evaluater, ()=>entity.Scale, (item, val) => item.Scale = val);
            public static Tween Scale(Entity entity, Vect2 final, float seconds, IEvaluate<float> evaluater = null)
                => Scale(entity, final, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween ScaleByPrivate(RenderEntity entity, Vect2 final, TimeSpan time, IEvaluate<float> evaluater = null)
               => MakeCustom(entity, () => final, time, evaluater, () => entity.PrivateScale, (item, val) => item.PrivateScale = val);
            public static Tween ScaleByPrivate(RenderEntity entity, Vect2 final, float seconds, IEvaluate<float> evaluater = null)
                => ScaleByPrivate(entity, final, TimeSpan.FromSeconds(seconds), evaluater);

            public static Tween Rotate(Entity entity, float value, TimeSpan time, IEvaluate<float> evaluater = null)
                => MakeCustom(entity, () => entity.SelfRotate.Angle + value, time, evaluater, () => entity.SelfRotate.Angle, (item, val) => item.SelfRotate = new Rotation(val));
            public static Tween Rotate(Entity entity, float value, float seconds, IEvaluate<float> evaluater = null)
                => Rotate(entity, value, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween RenderRotate(RenderEntity entity,float value, TimeSpan time,IEvaluate<float> evaluater = null)
                => MakeCustom(entity, () => entity.RenderRotation.Angle + value, time, evaluater, () => entity.RenderRotation.Angle, (item, val) => item.RenderRotation = new Rotation(val));
            public static Tween RenderRotate(RenderEntity entity, float value, float seconds, IEvaluate<float> evaluater = null)
                => RenderRotate(entity, value, seconds, evaluater);
            public static Tween Move(Entity entity, Vect2 to, TimeSpan time, IEvaluate<float> evaluater = null)
                 => MakeCustom(entity, () => to +entity.Pos, time, evaluater, () => entity.Pos, (item, val) => item.Pos = val);
            public static Tween Move(Entity entity, Vect2 to, float seconds, IEvaluate<float> evaluater = null)
                => Move(entity, to, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween MoveX(Entity entity, float to, TimeSpan time, IEvaluate<float> evaluater = null)
              => MakeCustom(entity, () => entity.Pos.X+to, time, evaluater, () => entity.Pos.X, (item, val) => item.Pos = new Vect2(val, item.Pos.Y));
            public static Tween MoveX(Entity entity, float to, float seconds, IEvaluate<float> evaluater = null)
                => MoveX(entity, to, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween MoveY(Entity entity, float to, TimeSpan time, IEvaluate<float> evaluater = null)
             => MakeCustom(entity, () => to +entity.Pos.Y, time, evaluater, () => entity.Pos.Y, (item, val) => item.Pos = new Vect2(item.Pos.X, val));
            public static Tween MoveY(Entity entity, float to, float seconds, IEvaluate<float> evaluater = null)
                => MoveY(entity, to, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween MoveTo(Entity entity, Vect2 final, TimeSpan time, IEvaluate<float> evaluater = null)
                => MakeCustom(entity, () => final, time, evaluater, () => entity.Pos, (item, val) => item.Pos = val);
            public static Tween MoveTo(Entity entity, Vect2 final, float seconds, IEvaluate<float> evaluater = null)
                => MoveTo(entity, final, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween MoveToX(Entity entity, float final, TimeSpan time, IEvaluate<float> evaluater = null)
                 => MakeCustom(entity, () => final, time, evaluater, () => entity.Pos.X, (item, val) => item.Pos = new Vect2(val,item.Pos.Y));
            public static Tween MoveToX(Entity entity, float final, float seconds, IEvaluate<float> evaluater = null)
                => MoveToX(entity, final, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween MoveToY(Entity entity, float final, TimeSpan time, IEvaluate<float> evaluater = null)
              => MakeCustom(entity, () => final, time, evaluater, () => entity.Pos.Y, (item, val) => item.Pos = new Vect2(item.Pos.X,val));
            public static Tween MoveToY(Entity entity, float final, float seconds, IEvaluate<float> evaluater = null)
                => MoveToY(entity, final, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween Alpha(RenderEntity entity, float final, TimeSpan time, IEvaluate<float> evaluater = null)
                => MakeCustom(entity, () => final, time, evaluater, () => entity.RenderColor.A/255f, (item, val) => item.SetAlpha(val));
            public static Tween Alpha(RenderEntity entity, float final, float seconds, IEvaluate<float> evaluater = null)
                => Alpha(entity, final, TimeSpan.FromSeconds(seconds), evaluater);
            public static Tween Color(RenderEntity entity, Color final, TimeSpan time, IEvaluate<float> evaluater = null)
                => MakeCustom(entity, () => final, time, evaluater, () => entity.RenderColor, (item, val) => item.RenderColor=val );
            public static Tween Color(RenderEntity entity, Color final, float seconds, IEvaluate<float> evaluater = null)
                => Color(entity, final, seconds, evaluater);
        }

        #endregion // STATIC
    }
}


