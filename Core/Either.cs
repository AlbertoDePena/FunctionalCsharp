using System;

namespace FunctionalCsharp
{
    public sealed class Either<TSuccess, TError>
    {
        private readonly TSuccess success;
        private readonly TError error;

        public bool IsSuccess { get; }

        public bool IsError => !IsSuccess;

        private Either(TSuccess item)
        {
            this.success = item ?? throw new ArgumentNullException(nameof(item));
            IsSuccess = true;
        }

        private Either(TError item)
        {
            this.error = item ?? throw new ArgumentNullException(nameof(item));
            IsSuccess = false;
        }

        public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TError, TResult> onError)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onError is null)
                throw new ArgumentNullException(nameof(onError));

            return IsSuccess ? onSuccess(success) : onError(error);
        }

        public static Either<TSuccess, TError> Success(TSuccess item) => new Either<TSuccess, TError>(item);

        public static Either<TSuccess, TError> Error(TError item) => new Either<TSuccess, TError>(item);
    }        
}