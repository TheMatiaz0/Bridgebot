using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public static class IEnumeratorCoroutineableExtesnion
    {


        /// <summary>
        /// Convert <see cref="IEnumerator{T}"/> of <see cref="ICoroutineable"/> to <see cref="Task"/>.
        /// <see cref="Task"/> will be stopped (by throwing <see cref="ObjectStatusChangedException"/>), if limit is killed.
        /// If you don't want it, use other overloading.
        /// </summary>
        public static async Task AsTask(this IEnumerator<ICoroutineable> enumerator,IKillable limit)
        {
            while (enumerator.MoveNext())
            {
                await enumerator.Current.AsTask(limit);
            }
        }
        /// <summary>
        /// Convert <see cref="ICoroutineable"/> to <see cref="Task"/>.
        /// <see cref="Task"/> won't be stoped even if object which it belong is killed. If you want to <see cref="Task"/> can stop, use other overloading.
        /// </summary>  
        public static async Task AsTask(this IEnumerator<ICoroutineable> enumerator )
        {
            await AsTask(enumerator, EObject.StaticMachine);
        }
    }
}
