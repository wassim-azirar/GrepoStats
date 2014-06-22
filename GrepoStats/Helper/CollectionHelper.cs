using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GrepoStats.Helper
{
    public static class CollectionHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new ObservableCollection<T>();
            foreach (var current in enumerable)
            {
                collection.Add(current);
            }

            return collection;
        }
    }
}
