using System;
using System.Collections.Generic;
using System.Linq;

namespace NobelLaureates.Ethereal.Messaging
{
    internal class MessengerService
    {
        private readonly Dictionary<Type, List<object>> _subscriptions = new Dictionary<Type, List<object>>();

        public void Publish<TMessage>(TMessage message, string topic)
        {
            var subs = GetSubscriptions<TMessage>();
            subs.ForEach(x => ((MessengerSubscription<TMessage>)x).TryExecute(message, topic));
        }

        public IDisposable Subscribe<TMessage>(Action<TMessage> action, string topic)
        {
            var sub = new MessengerSubscription<TMessage>(action, topic);
            ActOnSubscriptions<TMessage>(x => x.Add(sub));
            return new DisposableAction(() => ActOnSubscriptions<TMessage>(x => x.Remove(sub)));
        }

        private List<object> GetSubscriptions<TMessage>()
        {
            List<object> subs;
            lock (_subscriptions)
            {
                var type = typeof(TMessage);
                if (!_subscriptions.TryGetValue(type, out subs))
                {
                    subs = new List<object>();
                    _subscriptions.Add(type, subs);
                }

                return _subscriptions[type].ToList();
            }
        }

        private void ActOnSubscriptions<TMessage>(Action<List<object>> action)
        {
            lock (_subscriptions)
            {
                List<object> subs;
                var type = typeof(TMessage);
                if (!_subscriptions.TryGetValue(type, out subs))
                {
                    subs = new List<object>();
                    _subscriptions.Add(type, subs);
                }

                action(subs);
            }
        }
    }
}
