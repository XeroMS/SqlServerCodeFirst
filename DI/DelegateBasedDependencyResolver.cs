using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DI.Interfaces;

namespace DI
{
    public class DelegateBasedDependencyResolver : IDependencyResolver
    {
        private readonly Func<Type, object> _getService;
        private readonly Func<Type, IEnumerable<object>> _getServices;

        public DelegateBasedDependencyResolver(Func<Type, object> getService, Func<Type, IEnumerable<object>> getServices)
        {
            _getService = getService;
            _getServices = getServices;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This method might throw exceptions whose type we cannot strongly link against; namely, ActivationException from common service locator")]
        public object GetService(Type type)
        {
            try
            {
                return _getService.Invoke(type);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type type)
        {
            return _getServices(type);
        }
    }
}
