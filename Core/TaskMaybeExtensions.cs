using System;
using System.Threading.Tasks;

namespace CSharp.Functional
{
    /// <inheritdocs/>
    public static class TaskMaybeExtensions
    {
        /// <summary>
        /// Map a function on something or return default on nothing
        /// </summary>
        /// <param name="source"></param>
        /// <param name="nothing"></param>
        /// <param name="something"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static async Task<TResult> Match<T, TResult>(
            this Task<Maybe<T>> source,
            TResult nothing,
            Func<T, TResult> something)
        {
            var maybe = await source;
            return maybe.Match(nothing, something);
        }

        /// <summary>
        /// Map a function on a Maybe Task
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static async Task<Maybe<TResult>> Select<T, TResult>(
            this Task<Maybe<T>> source,
            Func<T, TResult> selector)
        {
            var maybe = await source;
            return maybe.Select(selector);
        }

        /// <summary>
        /// Bind a function on a Maybe Task
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static async Task<Maybe<TResult>> SelectMany<T, TResult>(
            this Task<Maybe<T>> source,
            Func<T, Task<Maybe<TResult>>> selector)
        {
            var maybe = await source;
            return await maybe.Match(
                nothing: Task.FromResult(Maybe<TResult>.Nothing()),
                something: x => selector(x));
        }

        /// <summary>
        /// Transform a Maybe into a Maybe Task
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static Task<Maybe<TResult>> Traverse<T, TResult>(
            this Maybe<T> source,
            Func<T, Task<TResult>> selector)
        {
            return source.Match(
                nothing: Task.FromResult(Maybe<TResult>.Nothing()),
                something: async x => Maybe<TResult>.Something(await selector(x)));
        }

        /// <summary>
        /// Create a Task from a Maybe object
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        public static Task<Maybe<T>> ToTask<T>(this Maybe<T> source)
        {
            return Task.FromResult(source);
        }
    }
}