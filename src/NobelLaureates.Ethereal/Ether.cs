using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NobelLaureates.Ethereal
{
    public class Ether : IEther
    {
        private List<IEtherActionListener> _listeners = new List<IEtherActionListener>();

        public EtherServiceContext<TService> RegisterService<TService>(EtherService<TService> etherService, TService service)
        {
            if (etherService == null) throw new ArgumentNullException(nameof(etherService));
            if (service == null) throw new ArgumentNullException(nameof(service));

            etherService.Register(this, service);
            return new EtherServiceContext<TService>(this, etherService);
        }

        public EtherActionContext<TRequest, TResponse> RegisterAction<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> etherAction,
            Func<TRequest, TResponse> execute)
        {
            if (etherAction == null) throw new ArgumentNullException(nameof(etherAction));
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            etherAction.Register(this, execute);
            return new EtherActionContext<TRequest, TResponse>(this, etherAction);
        }

        public TResponse Execute<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> etherAction,
            TRequest request)
        {
            if (etherAction == null) throw new ArgumentNullException(nameof(etherAction));

            var listeners = _listeners.ToList();

            listeners.ForEach(l => l.OnExecuting(etherAction, request));

            TResponse response = default(TResponse);
            try
            {
                response = etherAction.Execute(this, request);
            }
            catch (Exception ex)
            {
                listeners.ForEach(l => l.OnError(etherAction, ex));
                throw;
            }

            listeners.ForEach(l => l.OnResponding(etherAction, response));

            return response;
        }

        public void RegisterListener(IEtherActionListener listener)
        {
            if (listener == null) throw new ArgumentNullException(nameof(listener));

            _listeners.Add(listener);
        }
    }
}
