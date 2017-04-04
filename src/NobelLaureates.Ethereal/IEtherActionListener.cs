using System;

namespace NobelLaureates.Ethereal
{
    public interface IEtherActionListener
    {
        void OnExecuting<TRequest, TResponse>(EtherAction<TRequest, TResponse> etherAction, TRequest request);

        void OnResponding<TRequest, TResponse>(EtherAction<TRequest, TResponse> etherAction, TResponse response);

        void OnError<TRequest, TResponse>(EtherAction<TRequest, TResponse> etherAction, Exception ex);
    }

    public interface IEtherActionListener<TRequest,TResponse>
    {
        void OnExecuting(EtherAction<TRequest, TResponse> etherAction, TRequest request);

        void OnResponding(EtherAction<TRequest, TResponse> etherAction, TResponse response);

        void OnError(EtherAction<TRequest, TResponse> etherAction, Exception ex);
    }
}
