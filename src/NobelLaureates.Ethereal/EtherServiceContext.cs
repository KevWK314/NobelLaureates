using System;

namespace NobelLaureates.Ethereal
{
    public class EtherServiceContext<TService>
    {
        private readonly IEther _ether;
        private readonly EtherService<TService> _etherService;

        internal EtherServiceContext(IEther ether, EtherService<TService> etherService)
        {
            _ether = ether;
            _etherService = etherService;
        }

        public EtherActionContext<TRequest, TResponse> RegisterAction<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> etherAction,
            Func<TService, TRequest, TResponse> execute)
        {
            if (etherAction == null) throw new ArgumentNullException(nameof(etherAction));
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            return _ether.RegisterAction(etherAction, r => execute(_etherService.GetService(_ether), r));
        }

        public IEther Ether()
        {
            return _ether;
        }
    }
}
