using System;
using System.Threading.Tasks;

namespace FunctionalCsharp
{
    public static class TaskMaybeExtensions
    {
        public static async Task<TResult> Match<T, TResult>(
            this Task<Maybe<T>> source,
            TResult nothing,
            Func<T, TResult> something)
        {
            var maybe = await source;
            return maybe.Match(nothing, something);
        }

        public static async Task<Maybe<TResult>> Select<T, TResult>(
            this Task<Maybe<T>> source,
            Func<T, TResult> selector)
        {
            var maybe = await source;
            return maybe.Select(selector);
        }

        public static async Task<Maybe<TResult>> SelectMany<T, TResult>(
            this Task<Maybe<T>> source,
            Func<T, Task<Maybe<TResult>>> selector)
        {
            var maybe = await source;
            return await maybe.Match(
                nothing: Task.FromResult(Maybe<TResult>.Nothing()),
                something: x => selector(x));
        }

        public static Task<Maybe<TResult>> Traverse<T, TResult>(
            this Maybe<T> source,
            Func<T, Task<TResult>> selector)
        {
            return source.Match(
                nothing: Task.FromResult(Maybe<TResult>.Nothing()),
                something: async x => Maybe<TResult>.Something(await selector(x)));
        }
    }
}