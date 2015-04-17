using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TimeSpan TimeElapsed { get; internal set; }
        public int RequestsProcessed { get; internal set; }
        public int TotalRequests { get; internal set; }
        public string LastResponse { get; internal set; }

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
                    return RequestsProcessed / TimeElapsed.TotalSeconds;
                }
                return 0;
            }
        }

        public int RequestsRemaining
        {
            get { return TotalRequests - RequestsProcessed; }
        }

        public TimeSpan TimeRemaining
        {
            get { return (DateTime.Now.AddSeconds((int)Math.Round(RequestsRemaining / RequestsPerSecond)) - DateTime.Now); }
        }
    }
}
