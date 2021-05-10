using System;
using System.Threading.Tasks;

namespace FunctionalCsharp
{
    public static class TaskExtensions
    {
        public async static Task<TResult> Map<T, TResult>(
            this Task<T> source,
            Func<T, TResult> selector)
        {
            var x = await source;
            return selector(x);
        }

        public async static Task<TResult> Bind<T, TResult>(
            this Task<T> source,
            Func<T, Task<TResult>> selector)
        {
            var x = await source;
            return await selector(x);
        }
    }
}