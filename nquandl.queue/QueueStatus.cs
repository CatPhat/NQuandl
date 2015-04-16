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
        public int RequestsProcessed { get; internal set; }
        public int TotalRequests { get; internal set; }

        public int RequestsRemaining
        {
            get { return TotalRequests - RequestsProcessed; }
        }

        public int TimeRemaining
        {
            get { return (300*RequestsRemaining) / 1000; }
        }
    }
}
