using System;
using NQuandl.Queue;

namespace NQuandl.TestConsole
{
    public static class Verbose
    {
        public static void PrintStatus()
        {
            Console.WriteLine("                     Total: {0}", NQueue.GetQueueStatus().TotalRequests);
            Console.WriteLine("                 Remaining: {0}", NQueue.GetQueueStatus().RequestsRemaining);
            Console.WriteLine("                 Processed: {0}", NQueue.GetQueueStatus().RequestsProcessed);
            Console.WriteLine("              Time Elapsed: {0}", NQueue.GetQueueStatus().TimeElapsed);
            Console.WriteLine("            Time Remaining: {0}", NQueue.GetQueueStatus().TimeRemaining);
            Console.WriteLine("            RequestsPerSec: {0} ", NQueue.GetQueueStatus().RequestsPerSecond);
            Console.WriteLine("10 Minutes at Current Rate: {0} ",
                NQueue.GetQueueStatus().HowManyRequestsIn10MinutesAtCurrentRate);
           // Console.WriteLine(NQueue.GetQueueStatus().LastResponse);
            //Console.WriteLine("{0}",new string('.', NQueue.GetQueueStatus().RequestsRemaining));
        }
    }
}