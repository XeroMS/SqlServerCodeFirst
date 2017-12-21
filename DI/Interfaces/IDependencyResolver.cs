using System;
using System.Collections.Generic;

namespace DI.Interfaces
{
    public interface IDependencyResolver
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }
}
