using System;

namespace NobelLaureates.Ethereal.Listeners
{
    public class EtherActionListenerBuilder<TRequest, TResponse>
    {
        private EtherActionListener<TRequest, TResponse> _listener;

        public EtherActionListenerBuilder(EtherAction<TRequest, TResponse> etherAction)
        {
            if (etherAction == null) throw new ArgumentNullException(nameof(etherAction));

            _listener = new EtherActionListener<TRequest, TResponse>(etherAction);
        }

        public EtherActionListenerBuilder<TRequest, TResponse> WhenExecuting(Action<EtherAction<TRequest, TResponse>, TRequest> onExecuting)
        {
            if (onExecuting == null) throw new ArgumentNullException(nameof(onExecuting));

            _listener.WhenExecuting(onExecuting);
            return this;
        }

        public EtherActionListenerBuilder<TRequest, TResponse> WhenResponding(Action<EtherAction<TRequest, TResponse>, TResponse> onResponding)
        {
            if (onResponding == null) throw new ArgumentNullException(nameof(onResponding));

            _listener.WhenResponding(onResponding);
            return this;
        }

        public EtherActionListenerBuilder<TRequest, TResponse> WhenError(Action<EtherAction<TRequest, TResponse>, Exception> onError)
        {
            if (onError == null) throw new ArgumentNullException(nameof(onError));

            _listener.WhenError(onError);
            return this;
        }
    }
}
