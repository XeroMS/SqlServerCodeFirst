using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DI.Interfaces;

namespace DI
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This method might throw exceptions whose type we cannot strongly link against; namely, ActivationException from common service locator")]
        public object GetService(Type serviceType)
        {
            // Since attempting to create an instance of an interface or an abstract type results in an exception, immediately return null
            // to improve performance and the debugging experience with first-chance exceptions enabled.
            if (serviceType.IsInterface || serviceType.IsAbstract)
            {
                return null;
            }

            try
            {
                return Activator.CreateInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }
    }
}
