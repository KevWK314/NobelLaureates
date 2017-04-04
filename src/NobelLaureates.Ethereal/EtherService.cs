using System;

namespace NobelLaureates.Ethereal
{
    public sealed class EtherService<TService>
    {
        internal EtherService(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public bool IsRegistered { get; private set; }

        internal TService Service { get; private set; }

        internal void Register(TService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            Service = service;
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
