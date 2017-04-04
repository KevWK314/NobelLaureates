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
            Func<TService, TRequest, TResponse> action)
        {
            if (etherAction == null)
            {
                throw new ArgumentNullException(nameof(etherAction));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return _ether.RegisterAction(etherAction, r => action(_etherService.Service, r));
        }

        public IEther Ether()
        {
            return _ether;
        }
    }
}
