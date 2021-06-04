using System;

namespace CSharp.Functional
{
    public static class MaybeExtensions
    {
        public static Maybe<TResult> Select<T, TResult>(
            this Maybe<T> source,
            Func<T, TResult> selector)
        {
            return source.Match(
                nothing: Maybe<TResult>.Nothing(),
                something: x => Maybe<TResult>.Something(selector(x)));
        }

        public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> source)
        {
            return source.Match(
                nothing: Maybe<T>.Nothing(),
                something: x => x);
        }

        public static Maybe<TResult> SelectMany<T, TResult>(
            this Maybe<T> source,
            Func<T, Maybe<TResult>> selector)
        {
            return source.Match(
                nothing: Maybe<TResult>.Nothing(),
                something: selector);
        }

        public static Maybe<TResult> SelectMany<T, U, TResult>(
            this Maybe<T> source,
            Func<T, Maybe<U>> k,
            Func<T, U, TResult> s)
        {
            return source
                .SelectMany(x => k(x)
                    .SelectMany(y => Maybe<TResult>.Something(s(x, y))));
        }

        public static Maybe<T> ToMaybe<T>(this T source)
        {
            return source == null ? Maybe<T>.Nothing() : Maybe<T>.Something(source);
        }

        public static Maybe<T> ToMaybe<T>(this T source, Func<T, bool> isNullFunc)
        {
            return isNullFunc(source) ? Maybe<T>.Nothing() : Maybe<T>.Something(source);
        }

        public static Maybe<T> ToMaybe<T>(this Nullable<T> source) where T : struct
        {
            return source.HasValue ? Maybe<T>.Something(source.Value) : Maybe<T>.Nothing();
        }
    }
}