using System;

namespace FunctionalCsharp
{
    public static class EitherExtensions
    {
        public static Either<TSuccess, TError> Map<TSuccess, TError>(
            this Either<TSuccess, TError> source,
            Func<TSuccess, TSuccess> selector)
        {
            return source.Match(
                onSuccess: success => Either<TSuccess, TError>.Success(selector(success)),
                onError: Either<TSuccess, TError>.Error);
        }

        public static Either<TSuccess, TError> MapError<TSuccess, TError>(
            this Either<TSuccess, TError> source,
            Func<TError, TError> selector)
        {
            return source.Match(
                onSuccess: Either<TSuccess, TError>.Success,
                onError: error => Either<TSuccess, TError>.Error(selector(error)));
        }

        public static Either<TSuccess, TError> Bind<TSuccess, TError>(
            this Either<TSuccess, TError> source,
            Func<TSuccess, Either<TSuccess, TError>> selector)
        {
            return source.Match(
                onSuccess: selector,
                onError: Either<TSuccess, TError>.Error);
        }
    }
}