using System;

namespace FunctionalCsharp
{
    public static class MaybeExtensions
    {
        public static Maybe<TResult> Map<T, TResult>(
            this Maybe<T> source,
            Func<T, TResult> selector)
        {
            return source.Match(
                nothing: Maybe<TResult>.Nothing(),
                something: x => Maybe<TResult>.Something(selector(x)));
        }

        public static Maybe<TResult> Bind<T, TResult>(
            this Maybe<T> source,
            Func<T, Maybe<TResult>> selector)
        {
            return source.Match(
                nothing: Maybe<TResult>.Nothing(), 
                something: selector);
        }
    }
}