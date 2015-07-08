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
            Console.WriteLine("            RequestsPerSec: {0}", NQueue.GetQueueStatus().RequestsPerSecond);
            Console.WriteLine("   Est Average RP10Minutes: {0}",
                NQueue.GetQueueStatus().HowManyRequestsIn10MinutesAtCurrentRate);
           // Console.WriteLine(NQueue.GetQueueStatus().LastResponse);
            //Console.WriteLine("{0}",new string('.', NQueue.GetQueueStatus().RequestsRemaining));
        }

        //public static void PrintTestResponse(TestJsonResponseV2 response)
        //{
        //   Console.Clear();
        //    var cached = response.CacheResponse;
        //    var timeElapsed =  cached.LastRequestTime - cached.FirstRequestTime;

        //    Console.WriteLine();
        //    Console.WriteLine("                   Processed: {0}", cached.RequestCount);
        //    Console.WriteLine("                Time Elapsed: {0}", timeElapsed);
        //    Console.WriteLine("              RequestsPerSec: {0}", cached.RequestsPerSecond);
        //    Console.WriteLine("             Est RP10Minutes: {0}", cached.EstimatedRequestsPer10MinutesAtCurrentRate);
        //    Console.WriteLine("     Est Average RP10Minutes: {0}", cached.EstimatedAverageRequestsPer10MinutesAtCurrentRate);
        //    Console.WriteLine("Average Time Between Requests {0}", cached.AverageTimeBetweenRequests);
        //    Console.WriteLine("          Requests Remaining: {0}", cached.RequestsRemaining );
        //    Console.WriteLine(" Average Requests Per Second: {0}", cached.AverageRequestsPerSecond);
        //}
    }
}