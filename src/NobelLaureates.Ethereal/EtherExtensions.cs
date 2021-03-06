﻿using System;
using System.Threading.Tasks;

namespace NobelLaureates.Ethereal
{
    public static class EtherExtensions
    {
        public static EtherServiceContext<TService> WithService<TService>(this IEther ether, EtherService<TService> etherService)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));
            if (etherService == null) throw new ArgumentNullException(nameof(etherService));

            return new EtherServiceContext<TService>(ether, etherService);
        }

        public static EtherActionContext<TRequest, TResponse> WithAction<TRequest, TResponse>(this IEther ether, EtherAction<TRequest, TResponse> etherAction)
        {
            if (ether == null) throw new ArgumentNullException(nameof(ether));
            if (etherAction == null) throw new ArgumentNullException(nameof(etherAction));

            return new EtherActionContext<TRequest, TResponse>(ether, etherAction);
        }

        public static Task<TResponse> ExecuteAsync<TRequest,TResponse>(
            this IEther ether, 
            EtherAction<TRequest, TResponse> action,
            TRequest request)
        {
            return Task.Run(() => ether.Execute(action, request));
        }
    }
}
