using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DI.Interfaces;

namespace DI
{
    public sealed class CacheDependencyResolver : IDependencyResolver
    {
        private readonly ConcurrentDictionary<Type, object> _cache = new ConcurrentDictionary<Type, object>();
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _cacheMultiple = new ConcurrentDictionary<Type, IEnumerable<object>>();
        private readonly Func<Type, object> _getServiceDelegate;
        private readonly Func<Type, IEnumerable<object>> _getServicesDelegate;
        private readonly IDependencyResolver _resolver;

        public CacheDependencyResolver(IDependencyResolver resolver)
        {
            _resolver = resolver;
            _getServiceDelegate = _resolver.GetService;
            _getServicesDelegate = _resolver.GetServices;
        }

        public object GetService(Type serviceType)
        {
            return _cache.GetOrAdd(serviceType, _getServiceDelegate);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _cacheMultiple.GetOrAdd(serviceType, _getServicesDelegate);
        }
    }
}
