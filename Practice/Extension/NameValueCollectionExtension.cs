using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Extension
{
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Flattens a <see cref="NameValueCollection"/> to a simple string <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [NotNull]
        public static IDictionary<string, string> ToSimpleDictionary([NotNull] this NameValueCollection collection)
        {
            CodeContract.ArgumentNotNull(collection, "collection");

            return collection.AllKeys.ToDictionary(key => key, key => collection[key]);
        }

        /// <summary>
        /// Gets the value as an <see cref="IEnumerable"/> handling splitting the string if needed.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="paramName"></param>
        /// <returns>Does not return null.</returns>
        public static IEnumerable<string> GetValueList([NotNull] this NameValueCollection collection,
                                                       [NotNull] string paramName)
        {
            CodeContract.ArgumentNotNull(collection, "collection");
            CodeContract.ArgumentNotNull(paramName, "paramName");

            return collection[paramName] == null
                       ? Enumerable.Empty<string>()
                       : collection[paramName].Split(',').AsEnumerable();
        }

        /// <summary>
        /// Gets the first value of <paramref name="paramName"/> in the collection or default (Null).
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static string GetFirstOrDefault([NotNull] this NameValueCollection collection, [NotNull] string paramName)
        {
            CodeContract.ArgumentNotNull(collection, "collection");
            CodeContract.ArgumentNotNull(paramName, "paramName");

            return collection.GetValueList(paramName).FirstOrDefault();
        }
    }
}