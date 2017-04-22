using System;
using System.Threading;

namespace NobelLaureates.Ethereal.Messaging
{
    public static class MessagingExtensions
    {
        private static int _initialised = 0;

        private static MessengerService _messenger;
        private static readonly EtherService<MessengerService> MessengerService = new EtherService<MessengerService>("Ether.Messenger.Service");

        public static void Publish<TMessage>(this IEther ether, TMessage message)
        {
            Publish(ether, message, null);
        }

        public static void Publish<TMessage>(this IEther ether, TMessage message, string topic)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));

            Initialise(ether);

            _messenger.Publish(message, topic);
        }

        public static IDisposable Subscribe<TMessage>(this IEther ether, Action<TMessage> action)
        {
            return Subscribe(ether, null, action);
        }

        public static IDisposable Subscribe<TMessage>(this IEther ether, string topic, Action<TMessage> action)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));
            if (action == null) throw new ArgumentNullException(nameof(action));

            Initialise(ether);

            var actionName = $"Ether.Messenger.Subscription.{typeof(TMessage).FullName}";

            EtherAction<Action<TMessage>, IDisposable> etherAction =
                EtherAction.Create<Action<TMessage>, IDisposable>(actionName);

            ether.WithService(MessengerService)
                .RegisterAction(etherAction, (service, request) => { return service.Subscribe(request, topic); });

            return ether.Execute(etherAction, action);
        }

        private static void Initialise(IEther ether)
        {
            if (Interlocked.CompareExchange(ref _initialised, 1, 0) == 0)
            {
                _messenger = new MessengerService();
                ether.RegisterService(MessengerService, _messenger);
            }
        }
    }
}
