using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NobelLaureates.Ethereal.Messaging
{
    internal class MessengerService
    {
        private readonly ConcurrentDictionary<Type, List<object>> _subscriptions = new ConcurrentDictionary<Type, List<object>>();

        public void Publish<TMessage>(TMessage message, string topic)
        {
            List<object> subs = _subscriptions.TryGetValue(typeof(TMessage), out subs) ? subs.ToList() : null;
            subs?.ForEach(x => ((MessengerSubscription<TMessage>)x).TryExecute(message, topic));
        }

        public IDisposable Subscribe<TMessage>(Action<TMessage> action, string topic)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var sub = new MessengerSubscription<TMessage>(action, topic);
            ActOnSubscriptions<TMessage>(x => x.Add(sub));
            return new DisposableAction(() => ActOnSubscriptions<TMessage>(x => x.Remove(sub)));
        }

        private void ActOnSubscriptions<TMessage>(Action<List<object>> action)
        {
            _subscriptions.AddOrUpdate(typeof(TMessage),
                k =>
                {
                    var list = new List<object>();
                    action(list);
                    return list;
                },
                (k, v) =>
                {
                    action(v);
                    return v;
                });
        }
    }
}
