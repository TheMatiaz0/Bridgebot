
using System;
using System.Collections.Generic;
using System.Threading;

namespace TrualityEngine.Core
{
    public class TrualityContext : SynchronizationContext
    {


        private readonly Queue<Pair<SendOrPostCallback, object>> queue = new Queue<Pair<SendOrPostCallback, object>>();
        public override void Post(SendOrPostCallback d, object state)
        {
            queue.Enqueue(new Pair<SendOrPostCallback, object>(d, state));

        }
        public void UpdateFrame()
        {


            Queue<Pair<SendOrPostCallback, object>> copy = new Queue<Pair<SendOrPostCallback, object>>(queue);
            queue.Clear();

            while (copy.Count > 0)
            {
                var pair = copy.Dequeue();
                try
                {
                    pair.First.Invoke(pair.Second);
                }
                catch(Exception e)
                {
                    if(e is ObjectIsKilledException ==false)
                        Console.WriteLine($"Async exception has thrown {e.Message}");
                }
                
            }


        }
    }
}
