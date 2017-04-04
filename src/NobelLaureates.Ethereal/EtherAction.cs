using System;
using System.Collections.Generic;

namespace NobelLaureates.Ethereal
{
    public sealed class EtherAction<TRequest, TResponse>
    {
        private List<IEtherActionListener<TRequest, TResponse>> _listeners = new List<IEtherActionListener<TRequest, TResponse>>();

        internal EtherAction(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public bool IsRegistered { get; private set; }

        internal Func<TRequest, TResponse> Action { get; private set; }

        internal void Register(Func<TRequest, TResponse> action)
        {
            IsRegistered = true;
            Action = action;
        }

        public void RegisterListener(IEtherActionListener<TRequest, TResponse> listener)
        {
            _listeners.Add(listener);
        }

        internal TResponse Execute(TRequest request)
        {
            if (!IsRegistered)
            {
                throw new InvalidOperationException("Action is not registered");
            }

            _listeners.ForEach(l => l.OnExecuting(this, request));

            TResponse response;
            try
            {
                response = Action(request);
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
