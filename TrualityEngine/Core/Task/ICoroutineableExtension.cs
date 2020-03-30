using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public static class ICoroutineableExtension
    {
       
     
        /// <summary>
        /// Convert <see cref="ICoroutineable"/> to <see cref="Task"/>.
        /// <see cref="Task"/> will be stopped (by throwing <see cref="ObjectIsKilledException"/>), if limit is killed.
        /// If you don't want it, use other overloading.
        /// </summary>
        public static  async Task AsTask(this ICoroutineable coroutineable,IKillable limit)
        {       
            do
            {
                while (limit.IsActive == false)
                    await Task.Yield();
                await Task.Yield();
                if(limit.IsKill)
                {
                    throw new ObjectIsKilledException(limit);
                }
            } while (limit.IsActive==false||coroutineable.GetIsDone() == false);

        }
        /// <summary>
        /// Convert <see cref="ICoroutineable"/> to <see cref="Task"/>.
        /// Task won't be stopped, even if the object to which it belongs will be killed. If you want to <see cref="Task"/> can stop, use other overloa  ding.
        /// You rarely  should use it. Using only it can be unsafe.
        /// </summary>  
        public static async Task AsTask(this ICoroutineable coroutineable)
        {
            await AsTask(coroutineable,EObject.StaticMachine);
        }
        public static ICoroutineable Coroutineable(this IEnumerator<ICoroutineable> enumerator,YieldUpdater yield)
        {
            return yield.Start(enumerator);
        }
    }
}
