using NobelLaureates.Ethereal.Listeners;
using System;

namespace NobelLaureates.Ethereal
{
    public class EtherActionContext<TRequest, TResponse>
    {
        private IEther _ether;
        private readonly EtherAction<TRequest, TResponse> _etherAction;

        internal EtherActionContext(IEther ether, EtherAction<TRequest, TResponse> etherAction)
        {
            _ether = ether;
            _etherAction = etherAction;
        }

        public EtherActionContext<TRequest, TResponse> Apply(Func<TResponse, TResponse> apply)
        {
            var originalAction = _etherAction.Action;
            _etherAction.Register(r => apply(originalAction(r)));

            return this;
        }

        public EtherActionContext<TRequest, TResponse> RegisterListener(IEtherActionListener<TRequest, TResponse> listener)
        {
            _etherAction.RegisterListener(listener);

            return this;
        }

        public EtherActionContext<TRequest, TResponse> AddListener(Action<EtherActionListenerBuilder<TRequest, TResponse>> buildListener)
        {
            var builder = new EtherActionListenerBuilder<TRequest, TResponse>(_etherAction);
            buildListener?.Invoke(builder);

            return this;
        }

        public IEther Ether()
        {
            return _ether;
        }
    }
}
