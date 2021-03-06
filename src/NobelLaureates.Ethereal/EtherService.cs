﻿using System;
using System.Collections.Concurrent;

namespace NobelLaureates.Ethereal
{
    public sealed class EtherService<TService>
    {
        private ConcurrentDictionary<IEther, TService> _services = new ConcurrentDictionary<IEther, TService>();

        internal EtherService(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Invalid name", nameof(name));

            Name = name;
        }

        public string Name { get; private set; }

        internal TService GetService(IEther ether)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));

            TService service;
            if (_services.TryGetValue(ether, out service))
            {
                return service;
            }
            return default(TService);
        }

        internal void Register(IEther ether, TService service)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _services[ether] = service;
        }
    }

    public static class EtherService
    {
        public static EtherService<T> Create<T>(string name)
        {
            return new EtherService<T>(name);
        }
    }
}
