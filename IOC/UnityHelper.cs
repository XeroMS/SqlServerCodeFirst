using DI;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Services;
using Services.Data.Context;
using Services.Data.UnitOfWork;
using Unity;
using Unity.Lifetime;

namespace IOC
{
    /// <summary>
    /// Bind the given interface in request scope
    /// </summary>
    public static class IocExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }

    /// <summary>
    /// The injection for Unity
    /// </summary>
    public static partial class UnityHelper
    {
        public static IUnityContainer Start()
        {
            var container = new UnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            var buildUnity = BuildUnityContainer(container);
            return buildUnity;
        }

        /// <summary>
        /// Inject
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer(UnityContainer container)
        {
            // register all your components with the container here

            // Database context, one per request, ensure it is disposed
            container.BindInRequestScope<IDatabaseContext, DatabaseContext>();
            container.BindInRequestScope<IUnitOfWorkManager, UnitOfWorkManager>();

            //Bind the various domain model services and repositories that e.g. our scripts require
            container.BindInRequestScope<ICacheService, CacheService>();
            container.BindInRequestScope<ILoggingService, LoggingService>();

            CustomBindings(container);

            return container;
        }

        static partial void CustomBindings(UnityContainer container);
    }
}
