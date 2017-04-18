using System;
using System.Collections.Generic;

namespace NobelLaureates.Ethereal.Messaging
{
    public static class MessagingExtensions
    {
        private static bool _initialised = false;

        private static MessengerService _messenger;
        private static readonly EtherService<MessengerService> MessengerService = new EtherService<MessengerService>("Ether.Messenger.Service");

        public static void Publish<TMessage>(this IEther ether, TMessage message, string topic)
        {
            Initialise(ether);

            _messenger.Publish(message, topic);
        }

        public static IDisposable Subscribe<TMessage>(this IEther ether, string topic, Action<TMessage> action)
        {
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
            if (!_initialised)
            {
                _initialised = true;
                _messenger = new MessengerService();
                ether.RegisterService(MessengerService, _messenger);
            }
        }
    }
}
