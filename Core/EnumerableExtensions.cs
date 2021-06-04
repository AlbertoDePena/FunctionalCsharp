using System.Collections.Generic;
using System.Linq;

namespace CSharp.Functional
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Choose<T>(this IEnumerable<Maybe<T>> source)
        {
            return source.SelectMany(maybe =>
                maybe.Match(
                    nothing: Enumerable.Empty<T>(),
                    something: x => Singleton<T>(x)));
        }

        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Singleton<T>(T element)
        {
            return new[] { element };
        }
    }
}