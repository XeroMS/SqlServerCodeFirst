using System;
using System.Collections.Generic;
using DI.Interfaces;
using Unity;

namespace IOC
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private const string ContextKey = "perRequestContainer";

        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return IsRegistered(serviceType) ? ChildContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (IsRegistered(serviceType))
            {
                yield return ChildContainer.Resolve(serviceType);
            }

            foreach (var service in ChildContainer.ResolveAll(serviceType))
            {
                yield return service;
            }
        }

        protected IUnityContainer ChildContainer
        {
            get
            {
                if (!(Domain.AppContext.Current.Items[ContextKey] is IUnityContainer childContainer))
                {
                    Domain.AppContext.Current.Items[ContextKey] = childContainer = _container.CreateChildContainer();
                }

                return childContainer;
            }
        }

        public static void DisposeOfChildContainer()
        {
            if (Domain.AppContext.Current.Items[ContextKey] is IUnityContainer childContainer)
            {
                childContainer.Dispose();
            }
        }

        private bool IsRegistered(Type typeToCheck)
        {
            var isRegistered = true;

            if (typeToCheck.IsInterface || typeToCheck.IsAbstract)
            {
                isRegistered = ChildContainer.IsRegistered(typeToCheck);

                if (!isRegistered && typeToCheck.IsGenericType)
                {
                    var openGenericType = typeToCheck.GetGenericTypeDefinition();

                    isRegistered = ChildContainer.IsRegistered(openGenericType);
                }
            }

            return isRegistered;
        }
    }
}
