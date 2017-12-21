using DI;

namespace Services.Data.Factories
{
    /// <summary>
    /// ServiceFactory is to replace the API, and an efficient way to get access to any interfaced service
    /// </summary>
    public static class ServiceFactory
    {
        public static THelper Get<THelper>()
        {
            if (Domain.AppContext.Current != null)
            {
                var key = string.Concat("factory-", typeof(THelper).Name);
                if (!Domain.AppContext.Current.Items.Contains(key))
                {
                    var resolvedService = DependencyResolver.Current.GetService<THelper>();
                    Domain.AppContext.Current.Items.Add(key, resolvedService);
                }
                return (THelper)Domain.AppContext.Current.Items[key];
            }
            return DependencyResolver.Current.GetService<THelper>();
        }
    }
}
