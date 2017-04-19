using System;

namespace NobelLaureates.Ethereal
{
    public interface IEther
    {
        EtherServiceContext<TService> RegisterService<TService>(EtherService<TService> etherService, TService service);

        EtherActionContext<TRequest, TResponse> RegisterAction<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> etherAction,
            Func<TRequest, TResponse> execute);

        TResponse Execute<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> etherAction,
            TRequest request);

        void RegisterListener(IEtherActionListener listener);
    }
}