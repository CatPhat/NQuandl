using System;
using System.Collections.Generic;

namespace NQuandl.Queue
{
    public delegate void QueueStatusDelegate(IEnumerable<string> status);

    public class QueueResponse
    {
        public QueueStatus QueueStatus { get; internal set; }
        public string StringResponse { get; internal set; }
    }

    
    public class QueueStatus
    {
        private readonly DateTime _queueStatusStartTime;

        public QueueStatus()
        {
            _queueStatusStartTime = DateTime.Now;
        }
        
        public int RequestsUnprocessed { get; set; }

        public int RequestsProcessed { get; set; }

        public string LastResponse { get; set; }

        public TimeSpan TimeElapsed
        {
            get { return DateTime.Now - _queueStatusStartTime; }
        }
        public double HowManyRequestsIn10MinutesAtCurrentRate
        {
            get { return RequestsPerSecond*600; }
        }

        public double RequestsPerSecond
        {
            get
            {
                if (RequestsProcessed != 0)
                {
                    return RequestsProcessed/TimeElapsed.TotalSeconds;
                }
                return 0;
            }
        }

        public int TotalRequests
        {
            get { return RequestsUnprocessed; }
        }

        public int RequestsRemaining
        {
            get { return TotalRequests - RequestsProcessed; }
        }

        public TimeSpan TimeRemaining
        {
            get
            {
                return (DateTime.Now.AddSeconds((int) Math.Round(RequestsRemaining/RequestsPerSecond)) - DateTime.Now);
            }
        }
    }
}