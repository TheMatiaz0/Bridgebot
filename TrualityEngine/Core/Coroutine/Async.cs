using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{

    /// <summary>
    /// Static class, which has base <see cref="Coroutine"/> action  by <see cref="ICoroutineable"/> interface
    /// </summary>
    public static class Async
    {

        /// <summary>
        /// Returns control in the next frame.
        /// </summary>
        /// <returns></returns>
        public static ICoroutineable WaitOneFrame()
        {
            return new WaitOneFrameY();
        }
        /// <summary>
        /// Returns control after time delay. TimeScale can affect to this.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ICoroutineable WaitTime(TimeSpan time)
        {
            return new WaitTimeY(time);
        }
        /// <summary>
        /// Returns control after time delay. TimeScale can affect to this.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ICoroutineable WaitTime(float seconds)
        {
            return new WaitTimeY(TimeSpan.FromSeconds(seconds));

        }
        /// <summary>
        /// Returns control after coroutine ending. It idendical to yield return coroutine.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static ICoroutineable WaitOther(Coroutine coroutine)
        {
            if (coroutine == null)
                throw new ArgumentNullException(nameof(coroutine));
            return new WaitOtherY(coroutine);
        }
        /// <summary>
        /// Wait until condition returns false.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        ///  /// <exception cref="ArgumentNullException"></exception>
        public static ICoroutineable WaitTo(Predicate<TimeSpan> condition)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));
            return new WaitToY(condition);
        }
        /// <summary>
        /// Wait until condition returns true.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICoroutineable Until(Predicate<TimeSpan> condition)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));
            return new WaitToY(t=>condition(t)==false);
        }
        /// <summary>
        /// Wait until task is running.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICoroutineable WaitToTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            return new WaitToY
                (t =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                        throw new TaskCanceledException(task);
                    return task.IsCompleted;
                });
        }

        private class WaitOneFrameY : ICoroutineable
        {
            public WaitOneFrameY()
            {

            }

            public  bool GetIsDone()
            {
                return true;
            }
        }
        private class WaitOtherY: ICoroutineable
        {
            public Coroutine Coroutine { get; }
            public WaitOtherY(Coroutine coroutine)
            {
                Coroutine = coroutine;
            }
            public bool GetIsDone()
            {
                return Coroutine.IsEnded == true;
            }
        }
        private class WaitTimeY : ICoroutineable
        {
            public TimeSpan TimeToWait { get; }
            private TimeSpan Actual { get; set; }

            public WaitTimeY(TimeSpan time)
            {
                TimeToWait = time;
                Actual = time;
            }
            public bool GetIsDone()
            {
                Actual -= TimeOfGame.Actual.DeltaTime;
                if (Actual <= TimeSpan.Zero)
                    return true;
                return false;
                
            }
        }
        private class WaitToY:ICoroutineable
        {
            public Predicate<TimeSpan> Condition { get; }
            public WaitToY(Predicate<TimeSpan> condition)
            {
                Condition = condition;
            }
            public bool GetIsDone()
            {
                return Condition(TimeOfGame.Actual.TotalTime);
            }
        }
       
        

    }
}
