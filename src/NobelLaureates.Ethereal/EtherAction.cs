using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NobelLaureates.Ethereal
{
    public sealed class EtherAction<TRequest, TResponse>
    {
        private ConcurrentDictionary<IEther, List<IEtherActionListener<TRequest, TResponse>>> _listeners =
            new ConcurrentDictionary<IEther, List<IEtherActionListener<TRequest, TResponse>>>();
        private ConcurrentDictionary<IEther, Func<TRequest, TResponse>> _registeredActions =
            new ConcurrentDictionary<IEther, Func<TRequest, TResponse>>();

        internal EtherAction(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Invalid name", nameof(name));

            Name = name;
        }

        public string Name { get; private set; }

        internal void Register(IEther ether, Func<TRequest, TResponse> execute)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            _registeredActions[ether] = execute;
        }

        public void RegisterListener(IEther ether, IEtherActionListener<TRequest, TResponse> listener)
        {
            if (listener == null) throw new ArgumentNullException(nameof(listener));

            _listeners.AddOrUpdate(ether,
                new List<IEtherActionListener<TRequest, TResponse>>(new[] { listener }),
                (k, v) =>
                {
                    v.Add(listener);
                    return v;
                });
        }

        internal bool IsRegistered(IEther ether)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));

            return _registeredActions.ContainsKey(ether);
        }

        internal Func<TRequest, TResponse> GetExecute(IEther ether)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));

            Func<TRequest, TResponse> execute;
            if (_registeredActions.TryGetValue(ether, out execute))
            {
                return execute;
            }
            return null;
        }

        internal TResponse Execute(IEther ether, TRequest request)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));

            Func<TRequest, TResponse> execute;
            if (!_registeredActions.TryGetValue(ether, out execute))
            {
                throw new InvalidOperationException("Action is not registered");
            }

            List<IEtherActionListener<TRequest, TResponse>> actionListeners;
            var listeners = _listeners.TryGetValue(ether, out actionListeners) ? actionListeners.ToList() : null;

            listeners?.ForEach(l => l.OnExecuting(this, request));

            TResponse response;
            try
            {
                response = execute(request);
            }
            catch (Exception ex)
            {
                listeners?.ForEach(l => l.OnError(this, ex));
                throw;
            }

            listeners?.ForEach(l => l.OnResponding(this, response));

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
