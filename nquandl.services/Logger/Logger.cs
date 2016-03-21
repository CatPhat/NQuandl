using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NQuandl.Services.Logger
{
    public class InboundRequestLogEntry
    {
        public string InboundRequestUri { get; set; }
        public DateTime StartTime { get; set; }
    }


    public class CompletedRequestLogEntry
    {
        public InboundRequestLogEntry InboundRequestLogEntry { get; set; }
    }

    public class Logger : ILogger
    {
        private static int InboundRequests;
        private static int CompletedRequests;
        private readonly ActionBlock<CompletedRequestLogEntry> _completedActionBlock;
        private readonly BufferBlock<CompletedRequestLogEntry> _completedRequestBlock;
        private readonly ActionBlock<InboundRequestLogEntry> _inboundActionBlock;

        private readonly BufferBlock<InboundRequestLogEntry> _inboundRequestBlock;

        private readonly BufferBlock<TimeSpan> _timeSpansBufferBlock;
        private readonly ActionBlock<TimeSpan> _timeSpansActionBlock;

        private readonly BlockingCollection<TimeSpan> _timeSpans;

        private TimeSpan GetAverageTimeSpan(int multiplier = 1)
        {
            return TimeSpan.FromMilliseconds(_timeSpans.Any() ? _timeSpans.Average(x => x.TotalMilliseconds*multiplier) : 0);
        }

        public Logger()
        {
            _timeSpans = new BlockingCollection<TimeSpan>();
            _inboundRequestBlock = new BufferBlock<InboundRequestLogEntry>();
            _completedRequestBlock = new BufferBlock<CompletedRequestLogEntry>();


            _inboundActionBlock = new ActionBlock<InboundRequestLogEntry>(item =>
            {
                InboundRequests = InboundRequests + 1;
                Log("Inbound Requests: " + InboundRequests);
            }, new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 1});

            _completedActionBlock = new ActionBlock<CompletedRequestLogEntry>(item =>
            {
                CompletedRequests = CompletedRequests + 1;
                var remaining = InboundRequests - CompletedRequests;
                NonBlockingConsole.WriteLine("-------------------------------");
                Log("Completed Request: " + item.InboundRequestLogEntry.InboundRequestUri);
                Log("Requests Remaining: " + remaining);
                Log("Estimated Time Left: " + GetAverageTimeSpan(remaining));
                NonBlockingConsole.WriteLine("-------------------------------");
            }, new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 1, BoundedCapacity = 1});

          
            _timeSpansBufferBlock = new BufferBlock<TimeSpan>();
            _timeSpansActionBlock = new ActionBlock<TimeSpan>(item =>
            {
                Log("Request Duration: " + item);
                _timeSpans.Add(item);
                Log("Average Request Duration: " + GetAverageTimeSpan());

            }
                , new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 1});

            _inboundRequestBlock.LinkTo(_inboundActionBlock);
            _completedRequestBlock.LinkTo(_completedActionBlock);
            _timeSpansBufferBlock.LinkTo(_timeSpansActionBlock);

           
        }


        public async Task AddInboundRequest(InboundRequestLogEntry entry)
        {
            await _inboundRequestBlock.SendAsync(entry);
        }

        public async Task AddCompletedRequest(CompletedRequestLogEntry entry)
        {
            await _completedRequestBlock.SendAsync(entry);
        }

        public async Task AddCompletedRequestDuration(TimeSpan timeSpan)
        {
            await _timeSpansBufferBlock.SendAsync(timeSpan);
        }


        public void Write(string logMessage)
        {
            Log(logMessage);
        }

        private void Log(string logMessage)
        {
            var now = DateTime.Now;
            NonBlockingConsole.WriteLine(string.Format("{0} {1} {2}", now, now.Ticks,
                logMessage));
        }
    }
}