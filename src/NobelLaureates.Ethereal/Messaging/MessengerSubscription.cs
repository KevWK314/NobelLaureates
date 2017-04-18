using System;

namespace NobelLaureates.Ethereal.Messaging
{
    internal class MessengerSubscription<TMessage>
    {
        private Action<TMessage> _publish;

        private string _topic;

        public MessengerSubscription(Action<TMessage> publish, string topic)
        {
            _publish = publish;
            _topic = topic;
        }

        public void TryExecute(TMessage message, string topic)
        {
            if(string.Equals(topic, _topic))
            {
                _publish(message);
            }
        }
    }
}
