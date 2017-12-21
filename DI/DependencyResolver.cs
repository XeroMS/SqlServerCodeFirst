using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DI.Interfaces;

namespace DI
{
    public class DependencyResolver
    {
        private static readonly DependencyResolver Instance = new DependencyResolver();

        private IDependencyResolver _current;
        private CacheDependencyResolver _currentCache;

        public static IDependencyResolver Current => Instance.InnerCurrent;

        internal static IDependencyResolver CurrentCache => Instance.InnerCurrentCache;

        public IDependencyResolver InnerCurrent => _current;

        internal IDependencyResolver InnerCurrentCache => _currentCache;

        public DependencyResolver()
        {
            InnerSetResolver(new DefaultDependencyResolver());
        }

        public static void SetResolver(IDependencyResolver resolver)
        {
            Instance.InnerSetResolver(resolver);
        }

        public void InnerSetResolver(IDependencyResolver resolver)
        {
            _current = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _currentCache = new CacheDependencyResolver(_current);
        }

        public void InnerSetResolver(object commonServiceLocator)
        {
            if (commonServiceLocator == null)
            {
                throw new ArgumentNullException(nameof(commonServiceLocator));
            }

            var locatorType = commonServiceLocator.GetType();
            var getInstance = locatorType.GetMethod("GetInstance", new[] { typeof(Type) });
            var getInstances = locatorType.GetMethod("GetAllInstances", new[] { typeof(Type) });

            if (getInstance == null ||
                getInstance.ReturnType != typeof(object) ||
                getInstances == null ||
                getInstances.ReturnType != typeof(IEnumerable<object>))
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, "Does not implement ICommonServiceLocator: {0}",
                        locatorType.FullName), nameof(commonServiceLocator));
            }

            var getService = (Func<Type, object>)Delegate.CreateDelegate(typeof(Func<Type, object>), commonServiceLocator, getInstance);
            var getServices = (Func<Type, IEnumerable<object>>)Delegate.CreateDelegate(typeof(Func<Type, IEnumerable<object>>), commonServiceLocator, getInstances);

            InnerSetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types.")]
        public void InnerSetResolver(Func<Type, object> getService, Func<Type, IEnumerable<object>> getServices)
        {
            if (getService == null)
            {
                throw new ArgumentNullException(nameof(getService));
            }
            if (getServices == null)
            {
                throw new ArgumentNullException(nameof(getServices));
            }

            InnerSetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }
    }
}
