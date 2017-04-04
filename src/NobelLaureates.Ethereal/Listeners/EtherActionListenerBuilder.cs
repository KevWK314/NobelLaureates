using System;

namespace NobelLaureates.Ethereal.Listeners
{
    public class EtherActionListenerBuilder<TRequest, TResponse>
    {
        private EtherActionListener<TRequest, TResponse> _listener;

        public EtherActionListenerBuilder(EtherAction<TRequest, TResponse> etherAction)
        {
            _listener = new EtherActionListener<TRequest, TResponse>(etherAction);
        }

        public EtherActionListenerBuilder<TRequest, TResponse> WhenExecuting(Action<EtherAction<TRequest, TResponse>, TRequest> onExecuting)
        {
            _listener.WhenExecuting(onExecuting);
            return this;
        }

        public EtherActionListenerBuilder<TRequest, TResponse> WhenResponding(Action<EtherAction<TRequest, TResponse>, TResponse> onResponding)
        {
            _listener.WhenResponding(onResponding);
            return this;
        }

        public EtherActionListenerBuilder<TRequest, TResponse> WhenError(Action<EtherAction<TRequest, TResponse>, Exception> onError)
        {
            _listener.WhenError(onError);
            return this;
        }
    }
}
