using System;
using System.Collections.Generic;

namespace NobelLaureates.Ethereal
{
    public sealed class EtherAction<TRequest, TResponse>
    {
        private List<IEtherActionListener<TRequest, TResponse>> _listeners = new List<IEtherActionListener<TRequest, TResponse>>();
        private Dictionary<IEther, Func<TRequest, TResponse>> _registeredActions = new Dictionary<IEther, Func<TRequest, TResponse>>();

        internal EtherAction(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        internal void Register(IEther ether, Func<TRequest, TResponse> action)
        {
            _registeredActions[ether] = action;
        }

        public void RegisterListener(IEtherActionListener<TRequest, TResponse> listener)
        {
            _listeners.Add(listener);
        }

        internal bool IsRegistered(IEther ether)
        {
            return _registeredActions.ContainsKey(ether);
        }

        internal Func<TRequest, TResponse> GetAction(IEther ether)
        {
            Func<TRequest, TResponse> action;
            if (_registeredActions.TryGetValue(ether, out action))
            {
                return action;
            }
            return null;
        }

        internal TResponse Execute(IEther ether, TRequest request)
        {
            Func<TRequest, TResponse> action;
            if (!_registeredActions.TryGetValue(ether, out action))
            {
                throw new InvalidOperationException("Action is not registered");
            }

            _listeners.ForEach(l => l.OnExecuting(this, request));

            TResponse response;
            try
            {
                response = action(request);
                _listeners.ForEach(l => l.OnResponding(this, response));
            }
            catch(Exception ex)
            {
                _listeners.ForEach(l => l.OnError(this, ex));
                throw;
            }
            return response;
        }
    }

    public static class EtherAction
    {
        public static EtherAction<TRequest, TResponse> Create<TRequest, TResponse>(string name)
        {
            return new EtherAction<TRequest, TResponse>(name);
        }
    }
}
