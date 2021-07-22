using System;

namespace CSharp.Functional
{
    /// <inheritdocs/>
    public static class MaybeExtensions
    {
        /// <summary>
        /// Map a function on a Maybe of something
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static Maybe<TResult> Select<T, TResult>(
            this Maybe<T> source,
            Func<T, TResult> selector)
        {
            return source.Match(
                nothing: Maybe<TResult>.Nothing(),
                something: x => Maybe<TResult>.Something(selector(x)));
        }

        /// <summary>
        /// Flatten out a Maybe within a Maybe
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> source)
        {
            return source.Match(
                nothing: Maybe<T>.Nothing(),
                something: x => x);
        }

        /// <summary>
        /// Bind a function to a Maybe of something
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static Maybe<TResult> SelectMany<T, TResult>(
            this Maybe<T> source,
            Func<T, Maybe<TResult>> selector)
        {
            return source.Match(
                nothing: Maybe<TResult>.Nothing(),
                something: selector);
        }

        /// <summary>
        /// Bind a function to a Maybe of something
        /// </summary>
        /// <param name="source"></param>
        /// <param name="k"></param>
        /// <param name="s"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static Maybe<TResult> SelectMany<T, U, TResult>(
            this Maybe<T> source,
            Func<T, Maybe<U>> k,
            Func<T, U, TResult> s)
        {
            return source
                .SelectMany(x => k(x)
                    .SelectMany(y => Maybe<TResult>.Something(s(x, y))));
        }

        /// <summary>
        /// Create a Maybe object
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        public static Maybe<T> ToMaybe<T>(this T source)
        {
            return source == null ? 
                Maybe<T>.Nothing() : 
                Maybe<T>.Something(source);
        }

        /// <summary>
        /// Create a Maybe object
        /// </summary>
        public static Maybe<T> ToMaybe<T>(this Nullable<T> source) where T : struct
        {
            return source.HasValue ? 
                Maybe<T>.Something(source.Value) : 
                Maybe<T>.Nothing();
        }

        /// <summary>
        /// Filter the Maybe object
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        public static Maybe<T> Filter<T>(this Maybe<T> source, Func<T, bool> predicate)
        {
            return source.Match(
                nothing: Maybe<T>.Nothing(),
                something: x => predicate(x) ? Maybe<T>.Something(x) : Maybe<T>.Nothing());
        }
    }
}