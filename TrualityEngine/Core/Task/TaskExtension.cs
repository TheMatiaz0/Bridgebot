using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrualityEngine.Core
{
    public static class TaskExtension
    {
        public static ICoroutineable ToCoroutinable(this Task task)
        {
            return Async.WaitToTask(task);
        }
    }
}
