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

        public EtherActionContext<TRequest, TResponse> Apply(Func<TResponse, TResponse> execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            var originalExecute = _etherAction.GetExecute(_ether);
            _etherAction.Register(_ether, r => execute(originalExecute(r)));

            return this;
        }

        public EtherActionContext<TRequest, TResponse> RegisterListener(IEtherActionListener<TRequest, TResponse> listener)
        {
            _etherAction.RegisterListener(_ether, listener);

            return this;
        }

        public EtherActionContext<TRequest, TResponse> AddListener(Action<EtherActionListenerBuilder<TRequest, TResponse>> buildListener)
        {
            if (buildListener == null) throw new ArgumentNullException(nameof(buildListener));

            var builder = new EtherActionListenerBuilder<TRequest, TResponse>(_etherAction);
            buildListener(builder);

            return this;
        }

        public IEther Ether()
        {
            return _ether;
        }
    }
}
