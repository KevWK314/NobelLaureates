using System;

namespace NobelLaureates.Ethereal
{
    public interface IEther
    {
        EtherServiceContext<TService> RegisterService<TService>(EtherService<TService> etherService, TService service);

        EtherActionContext<TRequest, TResponse> RegisterAction<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> etherAction,
            Func<TRequest, TResponse> action);

        TResponse Execute<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> action,
            TRequest request);

        void RegisterListener(IEtherActionListener listener);
    }
}