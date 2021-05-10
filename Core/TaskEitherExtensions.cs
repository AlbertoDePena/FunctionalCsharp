using System;
using System.Threading.Tasks;

namespace FunctionalCsharp
{
    public static class TaskEitherExtensions
    {
        public static async Task<TResult> Match<TSuccess, TError, TResult>(
            this Task<Either<TSuccess, TError>> source,
            Func<TSuccess, TResult> onSuccess, 
            Func<TError, TResult> onError)
        {
            var either = await source;
            return either.Match(onSuccess, onError);
        }

        public static async Task<Either<TSuccess, TError>> Map<TSuccess, TError>(
            this Task<Either<TSuccess, TError>> source,
            Func<TSuccess, TSuccess> selector)
        {
            var either = await source;
            return either.Map(selector);
        }

        public static async Task<Either<TSuccess, TError>> MapError<TSuccess, TError>(
            this Task<Either<TSuccess, TError>> source,
            Func<TError, TError> selector)
        {
            var either = await source;
            return either.MapError(selector);
        }

        public static async Task<Either<TSuccess, TError>> Bind<TSuccess, TError>(
            this Task<Either<TSuccess, TError>> source,
            Func<TSuccess, Task<Either<TSuccess, TError>>> selector)
        {
            var either = await source;
            return await either.Match(
                onSuccess: success => selector(success),
                onError: error => Task.FromResult(Either<TSuccess, TError>.Error(error))
            );
        }

         public static Task<Either<TSuccess, TError>> Traverse<TSuccess, TError>(
            this Either<TSuccess, TError> source,
            Func<TSuccess, Task<TSuccess>> selector)
        {
            return source.Match(
                onSuccess: async success => Either<TSuccess, TError>.Success(await selector(success)),
                onError: error => Task.FromResult(Either<TSuccess, TError>.Error(error))
            );
        }
    }
}