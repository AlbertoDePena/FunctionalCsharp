using System;
using System.Threading.Tasks;

namespace CSharp.Functional
{
    /// <inheritdocs/>
    public static class TaskExtensions
    {
        /// <summary>
        /// Map a function to a Task
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public async static Task<TResult> Select<T, TResult>(
            this Task<T> source,
            Func<T, TResult> selector)
        {
            var x = await source;
            return selector(x);
        }

        /// <summary>
        /// Bind a function to a Task
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public async static Task<TResult> SelectMany<T, TResult>(
            this Task<T> source,
            Func<T, Task<TResult>> selector)
        {
            var x = await source;
            return await selector(x);
        }
    }
}