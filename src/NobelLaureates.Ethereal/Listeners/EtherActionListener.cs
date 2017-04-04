using System;

namespace NobelLaureates.Ethereal.Listeners
{
    internal class EtherActionListener<TRequest, TResponse> : IEtherActionListener<TRequest,TResponse>
    {
        private readonly EtherAction<TRequest, TResponse> _etherAction;
        private Action<EtherAction<TRequest, TResponse>, Exception> _onError;
        private Action<EtherAction<TRequest, TResponse>, TRequest> _onExecuting;
        private Action<EtherAction<TRequest, TResponse>, TResponse> _onResponding;

        public EtherActionListener(EtherAction<TRequest, TResponse> etherAction)
        {
            _etherAction = etherAction;
        }

        public void OnError(EtherAction<TRequest, TResponse> etherAction, Exception ex)
        {
            _onError?.Invoke(_etherAction, ex);
        }

        public void OnExecuting(EtherAction<TRequest, TResponse> etherAction, TRequest request)
        {
            _onExecuting?.Invoke(_etherAction, request);
        }

        public void OnResponding(EtherAction<TRequest, TResponse> etherAction, TResponse response)
        {
            _onResponding?.Invoke(_etherAction, response);
        }

        internal void WhenExecuting(Action<EtherAction<TRequest, TResponse>, TRequest> onExecuting)
        {
            _onExecuting = onExecuting;
        }

        internal void WhenResponding(Action<EtherAction<TRequest, TResponse>, TResponse> onResponding)
        {
            _onResponding = onResponding;
        }

        internal void WhenError(Action<EtherAction<TRequest, TResponse>, Exception> onError)
        {
            _onError = onError;
        }
    }
}
