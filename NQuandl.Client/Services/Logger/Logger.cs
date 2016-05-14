using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Timers;

namespace NQuandl.Client.Services.Logger
{
    public class InboundRequestLogEntry
    {
        public string InboundRequestUri { get; set; }
        public DateTime StartTime { get; set; }
    }


    public class CompletedRequestLogEntry
    {
        public string CompletedRequestUri { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan Duration => EndTime - StartTime;
    }

    public class Logger : ILogger
    {

        private static int _completedRequestCounter;

        private readonly BufferBlock<InboundRequestLogEntry> _inboundRequests;
        private readonly ActionBlock<InboundRequestLogEntry> _inboundRequestsActionBlock; 
        private readonly BufferBlock<CompletedRequestLogEntry> _completedRequests;
        private readonly ActionBlock<CompletedRequestLogEntry> _completedRequestsActionBlock;

        private readonly ConcurrentQueue<InboundRequestLogEntry> _inboundQueue;
        private ConcurrentQueue<CompletedRequestLogEntry> _completedQueue;

        private readonly Stopwatch _appTimer;

        public Logger()
        {
            //_completedRequestCounter = 0;
            //_appTimer = new Stopwatch();
            //_appTimer.Start();

            //var timer = new System.Timers.Timer { Interval = 1000};
            //timer.Elapsed += TimerOnElapsed;
            ////timer.Enabled = true;


            //_inboundQueue = new ConcurrentQueue<InboundRequestLogEntry>();
            //_completedQueue = new ConcurrentQueue<CompletedRequestLogEntry>();
            //_inboundRequests = new BufferBlock<InboundRequestLogEntry>();
            //_completedRequests = new BufferBlock<CompletedRequestLogEntry>();

            //_inboundRequestsActionBlock = new ActionBlock<InboundRequestLogEntry>(x =>
            //{
            //    _inboundQueue.Enqueue(x);
            //});
           
            //_completedRequestsActionBlock = new ActionBlock<CompletedRequestLogEntry>(item =>
            //{
            //    InboundRequestLogEntry inboundRequest;
            //    _inboundQueue.TryDequeue(out inboundRequest);
            //    _completedQueue.Enqueue(item);
            //    IncrementCompletedRequestCounter();
            //});

            //_inboundRequests.LinkTo(_inboundRequestsActionBlock);
            //_completedRequests.LinkTo(_completedRequestsActionBlock);

        }


        private int CompletedRequestsFromTheLastSecond
        {
            get { return _completedQueue.Count(x => x.EndTime >= DateTime.Now - TimeSpan.FromSeconds(1)); }
        } 

        private static int IncrementCompletedRequestCounter()
        {
            return Interlocked.Increment(ref _completedRequestCounter);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
           
            NonBlockingConsole.WriteLine("clear");
            Log("Time Elapsed: " + _appTimer.Elapsed);
            Log("Inbound Requests: " + _inboundQueue.Count);
            Log("Average Duration: " + GetAverageTimeSpan());
            Log("Completed Requests: " + _completedRequestCounter);

            if (!_completedQueue.Any())
                return;
            var lastRequest = _completedQueue.Last();
            Log("Completed Requests Per Second: " + CompletedRequestsFromTheLastSecond);
            Log("Last Completed Request: " + lastRequest.CompletedRequestUri);
            Log("Last Completed Request: " + lastRequest.EndTime);

            if (_completedQueue.Count <= 10000)
                return;
            for (int i = 0; i <= _completedQueue.Count - 10000; i++)
            {
                CompletedRequestLogEntry completedRequest;
                _completedQueue.TryDequeue(out completedRequest);
            }
        }


        public async Task AddInboundRequest(InboundRequestLogEntry entry)
        {
            await _inboundRequests.SendAsync(entry);
        }

        public async Task AddCompletedRequest(CompletedRequestLogEntry entry)
        {
            await _completedRequests.SendAsync(entry);
        }

       
        public void Write(string logMessage)
        {
            Log(logMessage);
        }

        private TimeSpan GetAverageTimeSpan()
        {

            return
                TimeSpan.FromMilliseconds(_completedQueue.Any() ? _completedQueue.Average(x => x.Duration.TotalMilliseconds) : 0);
        }

        private void Log(string logMessage)
        {
            var now = DateTime.Now;
            
            NonBlockingConsole.WriteLine(string.Format("{0} {1} {2}", now, now.Ticks,
                logMessage));
        }
    }
}