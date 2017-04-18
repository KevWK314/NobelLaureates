using System;
using System.Collections.Generic;

namespace NobelLaureates.Ethereal
{
    public class Ether : IEther
    {
        private Dictionary<string, object> _services = new Dictionary<string, object>();
        private Dictionary<string, object> _actions = new Dictionary<string, object>();
        private List<IEtherActionListener> _listeners = new List<IEtherActionListener>();

        public EtherServiceContext<TService> RegisterService<TService>(EtherService<TService> etherService, TService service)
        {
            etherService.Register(this, service);
            _services[etherService.Name] = etherService;

            return new EtherServiceContext<TService>(this, etherService);
        }

        public EtherActionContext<TRequest, TResponse> RegisterAction<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> etherAction,
            Func<TRequest, TResponse> action)
        {
            etherAction.Register(this, action);
            _actions[etherAction.Name] = etherAction;

            return new EtherActionContext<TRequest, TResponse>(this, etherAction);
        }

        public TResponse Execute<TRequest, TResponse>(
            EtherAction<TRequest, TResponse> action,
            TRequest request)
        {
            _listeners.ForEach(l => l.OnExecuting(action, request));

            TResponse response = default(TResponse);
            try
            {
                response = action.Execute(this, request);
                _listeners.ForEach(l => l.OnResponding(action, response));
            }
            catch (Exception ex)
            {
                _listeners.ForEach(l => l.OnError(action, ex));
            }
            return response;
        }

        public void RegisterListener(IEtherActionListener listener)
        {
            _listeners.Add(listener);
        }
    }
}
