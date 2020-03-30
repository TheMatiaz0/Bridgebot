using System.Collections.Generic;
using System;
namespace TrualityEngine.Core
{

    /// <summary>
    /// Represent Coroutine. If you start use <see cref="YieldUpdater.Start(IEnumerator{ICoroutineable})"/>, you will get this.
    /// You can stop it and anything else.
    /// </summary>
    public sealed class Coroutine : ICoroutineable
    {
        /// <summary>
        /// Did <see cref="Coroutine"/> finished work?
        /// </summary>
        public bool IsEnded { get; private set; }
        /// <summary>
        /// Base enumerator. You probably shouldn't manual iterate over it.
        /// </summary>
        public IEnumerator<ICoroutineable> Enumerator { get; }
        /// <summary>
        /// Yield updater which has or had this Coroutine.
        /// </summary>
        public YieldUpdater YieldUpdater { get; }
    
        /// <exception cref="ArgumentNullException"></exception>
    
        internal Coroutine(YieldUpdater yieldUpdater, IEnumerator<ICoroutineable> enumerator)
        {
            YieldUpdater = yieldUpdater ?? throw new ArgumentNullException(nameof(yieldUpdater));
            Enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }
        /// <summary>
        /// If you cancel it the nearest "yield return" Coroutine will be stopped.
        /// </summary>
        /// <returns></returns>
        public bool Cancel()
        {

            bool result = IsEnded == false;
            if (result)
            {
                YieldUpdater.Disconnect(this);
                IsEnded = true;
            }

            return result;


        }


        bool ICoroutineable.GetIsDone()
        {
            return IsEnded;
        }
    }
}
