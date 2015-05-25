using System;
using System.Threading;

namespace NQuandl.Queue.Bus
{
    internal sealed class SendMessageRequest
    {
        public SendMessageRequest(object payload, CancellationToken cancellationToken)
            : this(payload, cancellationToken, success => { })
        {
        }

        public SendMessageRequest(object payload, CancellationToken cancellationToken, Action<bool> onSendComplete)
        {
            if (payload == null) throw new NullReferenceException("payload");
            if (cancellationToken == null) throw new NullReferenceException("cancellationToken");
            if (onSendComplete == null) throw new NullReferenceException("onSendComplete");

            Payload = payload;
            CancellationToken = cancellationToken;
            OnSendComplete = onSendComplete;
        }

        public object Payload { get; private set; }
        public CancellationToken CancellationToken { get; private set; }
        public Action<bool> OnSendComplete { get; private set; }
    }
}