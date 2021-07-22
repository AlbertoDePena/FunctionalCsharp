using System.Collections.Generic;
using System.Linq;

namespace CSharp.Functional
{
    /// <inheritdocs/>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Select the items in the IEnumerable that are something
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> Choose<T>(this IEnumerable<Maybe<T>> source)
        {
            return source.SelectMany(maybe =>
                maybe.Match(
                    nothing: Enumerable.Empty<T>(),
                    something: x => Singleton<T>(x)));
        }

        /// <summary>
        /// Return the IEnumerable or an empty one if the original is null
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Return an IEnumerable that contains one element
        /// </summary>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> Singleton<T>(this T element)
        {
            return new[] { element };
        }
    }
}