using System.Collections.Generic;
using System.Linq;

namespace FunctionalCsharp
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Choose<T>(this IEnumerable<Maybe<T>> source)
        {
            return source.SelectMany(maybe => 
                maybe.Match(
                    nothing:  System.Linq.Enumerable.Empty<T>(), 
                    something: x =>  Enumerable.Singleton<T>(x)));
        }

        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ??  System.Linq.Enumerable.Empty<T>();
        }
    }

    public static class Enumerable
    {
         public static IEnumerable<T> Singleton<T>(T element)
         {
             return new [] { element };
         }
    }
}