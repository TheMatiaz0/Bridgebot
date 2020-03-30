using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;

namespace TrualityEngine.Core
{

    /// <summary>
    /// Updates <see cref="Coroutine"/> and stops it if object that is connected to updater dies
    /// </summary>
    public sealed class YieldUpdater
    {
        /// <summary>
        /// If this dies, coroutines will stop
        /// </summary>
        public EObject Limit { get; }

        private List<Coroutine> Proceses { get; } = new List<Coroutine>();
      
        public IReadOnlyList<Coroutine> GetAllProcesses() => Proceses;
        
        public YieldUpdater(EObject limit )
        {
            Limit = limit;
        }
        /// <summary>
        /// Starts <see cref="Coroutine"/>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Coroutine Start(IEnumerator<ICoroutineable> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            Coroutine coroutine = new Coroutine(this, action);
            Proceses.Add(coroutine);
            return coroutine;
        }
       
        public void Update()
        {
            foreach (Coroutine coroutine in Proceses.ToArray())
            {
                if (Limit.IsKill == true)
                    Remove(coroutine);
            
                else if (coroutine.Enumerator.Current == null || coroutine.Enumerator.Current.GetIsDone())
                    if (coroutine.Enumerator.MoveNext() == false)
                        Remove(coroutine);

            }
            void Remove(Coroutine coroutine)
            {
                coroutine.Enumerator.Dispose();
                Proceses.Remove(coroutine);
                coroutine.Cancel();

            }
               

        }
        
        internal void Disconnect(Coroutine coroutine)
        {       
            Proceses.Remove(coroutine);

           
        }
       
      


    }
    
}
