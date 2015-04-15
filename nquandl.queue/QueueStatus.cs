using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Queue
{
    public delegate void QueueStatusDelegate(QueueStatus status);

    public class QueueResponse
    {
        public QueueStatus QueueStatus { get; internal set; }
        public string StringResponse { get; internal set; }
    }

    public class QueueStatus
    {
        public int RequestsRemaining { get; internal set; }
        public int RequestsProcessed { get; internal set; }

        public int TimeRemaining
        {
            get { return new DateTime(300).Millisecond*RequestsRemaining; }
        }
    }
}
