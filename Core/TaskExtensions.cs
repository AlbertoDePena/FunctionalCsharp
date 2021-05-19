using System;
using System.Threading.Tasks;

namespace CSharp.Functional
{
    public static class TaskExtensions
    {
        public async static Task<TResult> Select<T, TResult>(
            this Task<T> source,
            Func<T, TResult> selector)
        {
            var x = await source;
            return selector(x);
        }

        public async static Task<TResult> SelectMany<T, TResult>(
            this Task<T> source,
            Func<T, Task<TResult>> selector)
        {
            var x = await source;
            return await selector(x);
        }
    }
}