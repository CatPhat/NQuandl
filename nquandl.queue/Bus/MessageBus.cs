using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NQuandl.Queue.Bus
{
    //http://stackoverflow.com/questions/14096614/creating-a-message-bus-with-tpl-dataflow
    public class MessageBus
    {
        private readonly ConcurrentQueue<Subscription> _handlersToSubscribe = new ConcurrentQueue<Subscription>();

        private readonly ActionBlock<Tuple<object, Action>> _messageProcessor;

        public MessageBus()
        {
            // subscriptions is accessed only from the (single-threaded) ActionBlock, so it is thread-safe
            var subscriptions = new List<Subscription>();

            _messageProcessor = new ActionBlock<Tuple<object, Action>>(
                async tuple =>
                {
                    var message = tuple.Item1;
                    var completedAction = tuple.Item2;

                    Subscription handlerToSubscribe;
                    while (_handlersToSubscribe.TryDequeue(out handlerToSubscribe))
                    {
                        subscriptions.Add(handlerToSubscribe);
                    }

                    foreach (var subscription in subscriptions)
                    {
                        await subscription.HandlerAction(message);
                    }

                    completedAction();
                });
        }

        public Task SendAsync<TMessage>(TMessage message)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            Action completedAction = () => taskCompletionSource.SetResult(true);

            _messageProcessor.Post(new Tuple<object, Action>(message, completedAction));

            return taskCompletionSource.Task;
        }

     
        private class Subscription
        {
            public Subscription(Guid id, Func<object, Task> handlerAction)
            {
                Id = id;
                HandlerAction = handlerAction;
            }

            public Guid Id { get; private set; }
            public Func<object, Task> HandlerAction { get; private set; }
        }
    }
}