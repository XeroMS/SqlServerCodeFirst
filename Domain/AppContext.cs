using System.Collections;

namespace Domain
{
    public sealed class AppContext
    {
        private Hashtable _items;
        private static AppContext _current;

        public IDictionary Items => _items ?? (_items = new Hashtable());

        public static AppContext Current
        {
            get => _current ?? (_current = new AppContext());
            set => _current = value;
        }
    }
}
