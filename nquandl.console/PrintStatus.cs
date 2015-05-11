using System;
using NQuandl.Queue;

namespace NQuandl.TestConsole
{
    public class PrintStatus
    {
        public void Print()
        {
            var lastRequestCount = 0;
            while (true)
            {
                var currentRequestCount = NQueue.GetQueueStatus().RequestsProcessed;
                if (NQueue.GetQueueStatus().RequestsRemaining == 0) break;
                if (currentRequestCount == lastRequestCount) continue;

                Console.Clear();
                Verbose.PrintStatus();
                lastRequestCount = currentRequestCount;
            }
        }
    }
}