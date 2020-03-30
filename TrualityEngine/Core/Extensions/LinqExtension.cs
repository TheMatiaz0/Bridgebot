using System;
using System.Collections.Generic;

namespace TrualityEngine.Core
{
    public static class LinqExtension
    {
        public static int? GetIndex<T>(this IEnumerable<T> collection, Func<T, bool> func)
        {
            int i = 0;
            foreach (T item in collection)
            {

                if (func(item))
                    return i;
                i++;
            }
            return null;

        }
    }
}
